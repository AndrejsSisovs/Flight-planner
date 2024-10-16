using System.Text.Json.Serialization;

namespace WebApplication1.models
{
    public class SearchFlightsRequest
    {
        [JsonPropertyName("from")]
        public string DepartureAirport { get; set; }
        [JsonPropertyName("to")]
        public string DestinationAirport { get; set; }
        [JsonPropertyName("departureDate")]
        public DateTime FlightDate { get; set; }
    }
}
