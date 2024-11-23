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

        public Flight? GetFullFlightById(int id)
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

        public IEnumerable<Flight> SearchFlights(string? searchRequestFrom, string? searchRequestTo, string? searchRequestDepartureDate)
        {
            var matchingFlights = new List<Flight>();

            foreach (var flight in _context.Flights.Include(f => f.From).Include(f => f.To))
            {
                if (flight.From == null || flight.To == null)
                {
                    continue;
                }

                if (flight.From.AirportCode == searchRequestFrom &&
                    flight.To.AirportCode == searchRequestTo &&
                    DateTime.TryParse(flight.DepartureTime, out DateTime flightDepartureDate) &&
                    flightDepartureDate.Date == DateTime.Parse(searchRequestDepartureDate).Date)
                {
                    matchingFlights.Add(flight);
                }
            }

            return matchingFlights;
        }

        //public IEnumerable<Flight> SearchFlights(UserSearchFlights searchRequest)
        //{
        //    var matchingFlights = new List<Flight>();

        //    foreach (var flight in _context.Flights.Include(f => f.From).Include(f => f.To))
        //    {
        //        if (flight.From == null || flight.To == null)
        //        {
        //            continue;
        //        }

        //        if (flight.From.AirportCode == searchRequest.From &&
        //            flight.To.AirportCode == searchRequest.To &&
        //            DateTime.TryParse(flight.DepartureTime, out DateTime flightDepartureDate) &&
        //            flightDepartureDate.Date == DateTime.Parse(searchRequest.DepartureDate).Date)
        //        {
        //            matchingFlights.Add(flight);
        //        }
        //    }

        //    return matchingFlights;
        //}
    }
}
