using System.Text.Json.Serialization;

namespace WebApplication1.models
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string? DepartureDate { get; set; }
    }
}
