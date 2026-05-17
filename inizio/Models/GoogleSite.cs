using System.Text.Json.Serialization;

namespace inizio.Models
{
    public class GoogleSite
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("link")]
        public string Link { get; set; } = string.Empty;

        [JsonPropertyName("snippet")]
        public string Snippet { get; set; } = string.Empty;

        [JsonPropertyName("position")]
        public int Position { get; set; }
    }
}
