using System.Net.Http.Json;
using System.Text.Json;
using LibraryManager.Models.Entities;

namespace LibraryManager.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://openlibrary.org";

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Book>> GetBooksBySubjectAsync(string subject, int? limit = null, int? offset = null)
        {
            try
            {
                var url = $"{_baseUrl}/subjects/{subject}.json";
                if (limit.HasValue || offset.HasValue)
                {
                    url += "?";
                    if (limit.HasValue) url += $"limit={limit}";
                    if (offset.HasValue) url += $"&offset={offset}";
                }

                var response = await _httpClient.GetFromJsonAsync<OpenLibraryResponse>(url);
                if (response?.Works == null)
                    return new List<Book>();

                var books = response.Works.Select(work =>
                {
                    var coverUrl = work.Cover_id != null ? $"https://covers.openlibrary.org/b/id/{work.Cover_id}-M.jpg" : string.Empty;

                    return new Book
                    {
                        Title = work.Title,
                        Author = work.Authors?.FirstOrDefault()?.Name ?? "Unknown Author",
                        CoverUrl = coverUrl,
                        Subjects = work.Subjects ?? Array.Empty<string>()
                    };
                }).ToList();

                return books;
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error fetching books for subject {subject}: {ex.Message}");
                return new List<Book>();
            }
        }

        private class OpenLibraryResponse
        {
            public List<Work> Works { get; set; } = new();
        }

        private class Work
        {
            public string Title { get; set; } = string.Empty;
            public List<Author> Authors { get; set; } = new();
            public int? Cover_id { get; set; }
            public string[]? Subjects { get; set; }
        }

        private class Author
        {
            public string Name { get; set; } = string.Empty;
        }
    }
}