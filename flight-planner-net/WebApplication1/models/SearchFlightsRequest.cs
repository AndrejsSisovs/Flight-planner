namespace WebApplication1.models
{
    public class SearchFlightsRequest
    {
        public AirportRequest From { get; set; }
        public AirportRequest To { get; set; }
        public string? DepartureTime { get; set; }
    }
}
