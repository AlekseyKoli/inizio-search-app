using System.Text.Json.Serialization;

namespace inizio.Models
{
    public class SearchResult
    {
        [JsonPropertyName("organic")]
        public List<GoogleSite> Sites { get; set; } = new List<GoogleSite>();

    }
}
