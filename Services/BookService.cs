using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                    var key = work.Key?.Replace("/works/", "") ?? string.Empty;

                    return new Book
                    {
                        Title = work.Title,
                        Author = work.Authors?.FirstOrDefault()?.Name ?? "Unknown Author",
                        CoverUrl = coverUrl,
                        Subjects = work.Subjects ?? Array.Empty<string>(),
                        Key = key
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

        public async Task<Book?> GetBookDetailsAsync(string bookKey)
        {
            try
            {
                var url = $"{_baseUrl}/works/{bookKey}.json";
                var response = await _httpClient.GetFromJsonAsync<WorkDetailsResponse>(url);

                if (response == null)
                    return null;

                var coverUrl = response.Covers?.FirstOrDefault() > 0
                    ? $"https://covers.openlibrary.org/b/id/{response.Covers[0]}-M.jpg"
                    : string.Empty;

                string description = "No description available";
                if (response.Description.ValueKind != JsonValueKind.Null)
                {
                    if (response.Description.ValueKind == JsonValueKind.String)
                    {
                        description = response.Description.GetString() ?? "No description available";
                    }
                    else if (response.Description.ValueKind == JsonValueKind.Object)
                    {
                        var descValue = JsonSerializer.Deserialize<DescriptionValue>(response.Description);
                        description = descValue?.Value ?? "No description available";
                    }
                }

                return new Book
                {
                    Title = response.Title,
                    Author = "Unknown Author", // We'll need to fetch author details separately
                    CoverUrl = coverUrl,
                    Key = response.Key,
                    Description = description,
                    Subjects = response.Subjects ?? Array.Empty<string>()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching book details for key {bookKey}: {ex.Message}");
                return null;
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
            public string Key { get; set; } = string.Empty;
        }

        private class Author
        {
            public string Name { get; set; } = string.Empty;
        }

        private class WorkDetailsResponse
        {
            public string Title { get; set; } = string.Empty;
            public string Key { get; set; } = string.Empty;
            public List<AuthorInfo> Authors { get; set; } = new();
            [JsonPropertyName("description")]
            public JsonElement Description { get; set; }
            public List<int>? Covers { get; set; }
            public string[]? Subjects { get; set; }
        }

        private class AuthorInfo
        {
            public AuthorDetails Author { get; set; } = new();
            public TypeInfo Type { get; set; } = new();
        }

        private class AuthorDetails
        {
            public string Key { get; set; } = string.Empty;
        }

        private class TypeInfo
        {
            public string Key { get; set; } = string.Empty;
        }

        private class DescriptionValue
        {
            public string Value { get; set; } = string.Empty;
        }
    }
}