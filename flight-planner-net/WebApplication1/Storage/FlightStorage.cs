using WebApplication1.models;

namespace WebApplication1.Storage
{
    public static class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 0;

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = ++_id;
            _flights.Add(flight);

            return flight;
        }

        public static void ClearFlights()
        {
            _flights.Clear();
        }

        public static bool FlightExists(Flight flight)
        {
            return _flights.Any(existingFlight =>
                existingFlight.From.AirportCode == flight.From.AirportCode &&
                existingFlight.To.AirportCode == flight.To.AirportCode &&
                existingFlight.Carrier == flight.Carrier &&
                existingFlight.DepartureTime == flight.DepartureTime &&
                existingFlight.ArrivalTime == flight.ArrivalTime);
        }

        public static bool FlightIsNull(Flight flight)
        {
            return string.IsNullOrEmpty(flight.Carrier) ||
                   flight.From == null ||
                   flight.To == null ||
                   string.IsNullOrEmpty(flight.From.AirportCode) ||
                   string.IsNullOrEmpty(flight.To.AirportCode) ||
                   string.IsNullOrEmpty(flight.DepartureTime) ||
                   string.IsNullOrEmpty(flight.ArrivalTime);
        }

        public static bool FlightStringComparision(Flight flight)
        {
            if (flight == null || flight.From == null || flight.To == null)
            {
                return false;
            }

            string departureAirportCode = flight.From.AirportCode?.Trim().ToLowerInvariant();
            string arrivalAirportCode = flight.To.AirportCode?.Trim().ToLowerInvariant();

            return departureAirportCode == arrivalAirportCode;
        }

        public static bool AreFlightDatesInvalid(Flight flight)
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

        public static bool FlightDeleted(int flightId)
        {
            var flight = _flights.FirstOrDefault(f => f.Id == flightId);

            if (flight != null)
            {
                _flights.Remove(flight);
                return true;
            }

            return false;
        }

    }
}
