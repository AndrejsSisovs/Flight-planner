using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService: IEntityService<Flight>
    {
        bool FlightExists(Flight flight);
        Flight? GetFullFlightById(int id);
        ServiceResult DeleteFlight(int id);
        IEnumerable<Flight> SearchFlights(string? searchRequestFrom, string? searchRequestTo, string? searchRequestDepartureDate);
    }
}
