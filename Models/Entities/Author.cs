using System.Text.Json;

namespace LibraryManager.Models.Entities
{
    public class Author
    {
        public string Name { get; set; } = string.Empty;
        public string? Birth_date { get; set; }
        public string? Death_date { get; set; }
        public List<string>? Alternate_names { get; set; }
        public string? Personal_name { get; set; }
        public string? Key { get; set; }
    }
}