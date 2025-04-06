using LibraryManager.Models.Entities;

namespace LibraryManager.Models;

public class AddBookModel
{
    public ReadingStatus Status { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;
}