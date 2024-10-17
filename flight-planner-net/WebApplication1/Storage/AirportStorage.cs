using System.Security.Cryptography;
using WebApplication1.models;

namespace WebApplication1.Storage
{
    public static class AirportStorage
    {
        private static List<Airport> _airports = new List<Airport>();
        private static readonly object _airportLock = new object();

        public static void AddAirport(Airport airport)
        {
            lock (_airportLock)
            {
                var existingAirport = _airports.FirstOrDefault(a => a.AirportCode == airport.AirportCode);

                if (existingAirport == null)
                {
                    _airports.Add(airport);
                }
            }
        }

        public static List<Airport> SearchAirports(string userInput)
        {
            userInput = userInput.Trim().ToLower();

            var matchingAirports = new List<Airport>();

            foreach (var airport in _airports)
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
