namespace LibraryManager.Models.Entities
{
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[] Subjects { get; set; } = Array.Empty<string>();
    }
}