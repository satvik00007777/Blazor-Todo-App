using System.Text.Json.Serialization;

namespace TodoWebApp.Models
{
    public class TMDbResponse
    {
        [JsonPropertyName("results")]
        public List<TMDbMovie> Results { get; set; } = new List<TMDbMovie>();
    }
}
