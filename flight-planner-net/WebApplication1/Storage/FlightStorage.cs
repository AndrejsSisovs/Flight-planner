using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.models;

namespace WebApplication1.Storage
{
    public class FlightStorage
    {
        private readonly FlightPlannerDbContext _context;

        public FlightStorage (FlightPlannerDbContext context)
        {
            _context = context;
        }

        private static readonly object _lockFlights = new object();

        public Flight AddFlight(Flight flight)
        {
            lock (_lockFlights)
            {
                if (FlightExists(flight))
                {
                    return null;
                }

                _context.Flights.Add(flight);
                _context.SaveChanges();

                return flight;
            }
        }

        public void ClearFlights()
        {
           _context.Flights.RemoveRange(_context.Flights);
           _context.Airports.RemoveRange(_context.Airports);
           _context.SaveChanges();
        }

        public bool FlightExists(Flight flight)
        {
            lock (_lockFlights)
            {
                return _context.Flights.Any(existingFlight =>
                    existingFlight.From.AirportCode == flight.From.AirportCode &&
                    existingFlight.To.AirportCode == flight.To.AirportCode &&
                    existingFlight.Carrier == flight.Carrier &&
                    existingFlight.DepartureTime == flight.DepartureTime &&
                    existingFlight.ArrivalTime == flight.ArrivalTime);
            }
        }

        public bool FlightIsNull(Flight flight)
        {
            return string.IsNullOrEmpty(flight.Carrier) ||
                   flight.From == null ||
                   flight.To == null ||
                   string.IsNullOrEmpty(flight.From.AirportCode) ||
                   string.IsNullOrEmpty(flight.To.AirportCode) ||
                   string.IsNullOrEmpty(flight.DepartureTime) ||
                   string.IsNullOrEmpty(flight.ArrivalTime);
        }

        public bool FlightStringComparision(Flight flight)
        {
            if (flight == null || flight.From == null || flight.To == null)
            {
                return false;
            }

            string departureAirportCode = flight.From.AirportCode?.Trim().ToLower();
            string arrivalAirportCode = flight.To.AirportCode?.Trim().ToLower();

            return departureAirportCode == arrivalAirportCode;
        }

        public bool AreFlightDatesInvalid(Flight flight)
        {
            if (string.IsNullOrEmpty(flight.DepartureTime) || string.IsNullOrEmpty(flight.ArrivalTime))
            {
                return true;
            }

            DateTime departureTime, arrivalTime;

            if (!DateTime.TryParse(flight.DepartureTime, out departureTime) ||
                !DateTime.TryParse(flight.ArrivalTime, out arrivalTime))
            {
                return true;
            }

            if (arrivalTime <= departureTime)
            {
                return true;
            }

            return false;
        }

        public bool FlightDeleted(int flightId)
        {
            lock (_lockFlights)
            {
                var flight = _context.Flights.FirstOrDefault(f => f.Id == flightId);

                if (flight != null)
                {
                    _context.Flights.Remove(flight);
                    return true;
                }

                return false;
            }
        }

        public List<Flight> SearchFlights(SearchFlightsRequest searchRequest)
        {
            var matchingFlights = new List<Flight>();

            foreach (var flight in _context.Flights.Include(f => f.From).Include(f => f.To))
            {
                if (flight.From == null || flight.To == null)
                {
                    continue;
                }

                if (flight.From.AirportCode == searchRequest.DepartureAirport &&
                    flight.To.AirportCode == searchRequest.DestinationAirport &&
                    DateTime.TryParse(flight.DepartureTime, out DateTime flightDepartureDate) &&
                    flightDepartureDate.Date == searchRequest.FlightDate.Date)
                {
                    matchingFlights.Add(flight);
                }
            }

            return matchingFlights;
        }

        public Flight FindFlightById(int id)
        {
            var returnedFlight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);

            if (returnedFlight == null)
            {
                return null;
            }

            return returnedFlight;
        }
    }
}
