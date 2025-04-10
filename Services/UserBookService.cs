using Microsoft.EntityFrameworkCore;
using LibraryManager.Models.Entities;
using LibraryManager.Data;

namespace LibraryManager.Services;

public class UserBookService
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BookService _bookService;

    public UserBookService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, BookService bookService)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _bookService = bookService;
    }

    private string GetCurrentUserId() => _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException();

    public async Task<List<UserBook>> GetUserBooksAsync()
    {
        var userId = GetCurrentUserId();
        var userBooks = await _context.UserBooks
            .Where(ub => ub.UserId == userId)
            .ToListAsync();

        // Load book details for each user book
        foreach (var userBook in userBooks)
        {
            try
            {
                userBook.Book = await _bookService.GetBookDetailsAsync(userBook.BookKey);
            }
            catch (Exception)
            {
                // If book details can't be loaded, continue with the next book
                continue;
            }
        }

        return userBooks;
    }

    public async Task AddUserBookAsync(string bookKey, ReadingStatus status, DateTime startDate)
    {
        var userId = GetCurrentUserId();
        var userBook = new UserBook
        {
            UserId = userId,
            BookKey = bookKey,
            Status = status,
            StartDate = startDate,
            EndDate = status == ReadingStatus.Completed ? startDate : null
        };

        _context.UserBooks.Add(userBook);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserBookStatusAsync(int userBookId, ReadingStatus status)
    {
        var userId = GetCurrentUserId();
        var userBook = await _context.UserBooks
            .FirstOrDefaultAsync(ub => ub.Id == userBookId && ub.UserId == userId);

        if (userBook != null)
        {
            userBook.Status = status;
            userBook.EndDate = status == ReadingStatus.Completed ? DateTime.Now : null;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUserBookAsync(int userBookId)
    {
        var userId = GetCurrentUserId();
        var userBook = await _context.UserBooks
            .FirstOrDefaultAsync(ub => ub.Id == userBookId && ub.UserId == userId);

        if (userBook != null)
        {
            _context.UserBooks.Remove(userBook);
            await _context.SaveChangesAsync();
        }
    }
}