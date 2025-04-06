using System.Text.Json;

namespace LibraryManager.Models.Entities
{
    public class ReadingCounts
    {
        public Counts Counts { get; set; } = new();
    }

    public class Counts
    {
        public int Want_to_read { get; set; }
        public int Currently_reading { get; set; }
        public int Already_read { get; set; }
    }
}