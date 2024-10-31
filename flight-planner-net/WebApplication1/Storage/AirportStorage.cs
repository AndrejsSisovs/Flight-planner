using FlightPlanner.Core.Models;
using FlightPlanner.Data;

namespace WebApplication1.Storage
{
    public class AirportStorage
    {
        private readonly FlightPlannerDbContext _context;

        public AirportStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        private static readonly object _airportLock = new object();

        public void AddAirport(Airport airport)
        {
            lock (_airportLock)
            {
                var existingAirport = _context.Airports.FirstOrDefault(a => a.AirportCode == airport.AirportCode);

                if (existingAirport == null)
                {
                    _context.Airports.Add(airport);
                    _context.SaveChanges();
                }
            }
        }

        public List<Airport> SearchAirports(string userInput)
        {
            userInput = userInput.Trim().ToLower();

            var matchingAirports = new List<Airport>();

            foreach (var airport in _context.Airports)
            {
                if (airport.AirportCode.ToLower().Contains(userInput) ||
                    airport.City.ToLower().Contains(userInput) ||
                    airport.Country.ToLower().Contains(userInput))
                {
                    matchingAirports.Add(airport);
                }
            }

            return matchingAirports;
        }
    }
}
