namespace LibraryManager.Models.Entities
{
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public Author Author { get; set; } = new();
        public string CoverUrl { get; set; } = string.Empty;
        public string Description { get; set; } = "No description available";
        public string Key { get; set; } = string.Empty;
        public ReadingCounts ReadingCounts { get; set; } = new();
        public Ratings Ratings { get; set; } = new();
    }
}