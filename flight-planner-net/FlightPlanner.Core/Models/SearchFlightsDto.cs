namespace FlightPlanner.Core.Models
{
    public class SearchFlightsDto
    {
        public string FromAirport { get; set; }
        public string ToAirport { get; set; }
        public string DepartureTime { get; set; }
    }
}
