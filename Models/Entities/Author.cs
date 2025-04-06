using System.Text.Json;

namespace LibraryManager.Models.Entities
{
    public class Author
    {
        public string Name { get; set; } = string.Empty;
        public string? Birth_date { get; set; } = string.Empty;
        public string? Death_date { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public JsonElement? Alternate_names { get; set; } = new();
    }
}