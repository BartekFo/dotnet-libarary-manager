namespace LibraryManager.Models.Entities
{
    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string CoverUrl { get; set; } = string.Empty;
        public string Description { get; set; } = "No description available";
        public string[] Subjects { get; set; } = Array.Empty<string>();
        public string Key { get; set; } = string.Empty;
        public int? FirstPublishYear { get; set; }
        public int EditionCount { get; set; }
        public bool HasFulltext { get; set; }
        public string[] InternetArchiveIds { get; set; } = Array.Empty<string>();
        public bool PublicScan { get; set; }
    }
}