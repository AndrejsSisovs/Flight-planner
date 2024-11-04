using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace Flightplanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(FlightPlannerDbContext context) : base(context)
        {
        }

        public Flight? GetFullFlightById (int id)
        {
           return _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public bool FlightExists(Flight flight)
        {
            return _context.Flights.Any(existingFlight =>
                existingFlight.From.AirportCode == flight.From.AirportCode &&
                existingFlight.To.AirportCode == flight.To.AirportCode &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime);
        }

        public ServiceResult DeleteFlight(int id)
        {
            var flight = GetById(id);

            if (flight == null)
            {
                return new ServiceResult(false);
            }

            return Delete(flight);
        }

    }
}
