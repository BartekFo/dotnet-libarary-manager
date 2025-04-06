using System.Text.Json;
using System.Text.Json.Serialization;
using System;

namespace LibraryManager.Models.Entities
{
    public class Ratings
    {
        public Summary Summary { get; set; } = new();
        [JsonPropertyName("counts")]
        public RatingCounts Counts { get; set; } = new();
    }

    public class Summary
    {
        private decimal _average;
        public decimal Average
        {
            get => _average;
            set => _average = Math.Round(value, 2);
        }
        public int Count { get; set; }
    }
    public class RatingCounts
    {
        [JsonPropertyName("1")]
        public int One { get; set; }
        [JsonPropertyName("2")]
        public int Two { get; set; }
        [JsonPropertyName("3")]
        public int Three { get; set; }
        [JsonPropertyName("4")]
        public int Four { get; set; }
        [JsonPropertyName("5")]
        public int Five { get; set; }
    }
}