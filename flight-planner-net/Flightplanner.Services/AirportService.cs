using FlightPlanner.Core.Models;
using FlightPlanner.Data;

namespace FlightPlanner.Core.Services
{
    public class AirportService : IAirportService
    {
        private readonly FlightPlannerDbContext _context;

        public AirportService(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Airport> SearchAirports(string search)
        {
            search = search.Trim().ToLower();
            return _context.Airports
                .Where(airport => airport.AirportCode.ToLower().Contains(search) ||
                                  airport.City.ToLower().Contains(search) ||
                                  airport.Country.ToLower().Contains(search))
                .ToList();
        }
    }
}
