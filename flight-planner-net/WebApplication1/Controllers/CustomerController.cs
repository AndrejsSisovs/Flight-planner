using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;
using WebApplication1.Storage;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightStorage _flightStorage;
        private readonly AirportStorage _airportStorage;

        public CustomerController(FlightStorage flightStorage, AirportStorage airportStorage)
        {
            _flightStorage = flightStorage;
            _airportStorage = airportStorage;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest();
            }

            var matchingAirports = _airportStorage.SearchAirports(search);

            if (!matchingAirports.Any())
            {
                return NotFound();
            }

            return Ok(matchingAirports);
        }

        //[Route("flights/search")]
        //[HttpPost]
        //public IActionResult SearchFlights(SearchFlightsRequest searchRequest)
        //{
        //    if (string.IsNullOrEmpty(searchRequest.DepartureAirport) ||
        //        string.IsNullOrEmpty(searchRequest.DestinationAirport) ||
        //        searchRequest.FlightDate == DateTime.MinValue ||
        //        searchRequest.DepartureAirport == searchRequest.DestinationAirport)
        //    {
        //        return BadRequest();
        //    }

        //    var matchingFlights = _flightStorage.SearchFlights(searchRequest);

        //    if (matchingFlights.Count() == 0)
        //    {
        //        return Ok(new { Page = 0, TotalItems = 0, Items = matchingFlights.Any() ? matchingFlights : new List<Flight>() });
        //    }

        //    return Ok(new { Page = 0, TotalItems = matchingFlights.Count(), Items = new List<Flight>() });
        //}

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightStorage.FindFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}
