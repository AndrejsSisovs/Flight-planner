using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Storage;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest();
            }

            var matchingAirports = AirportStorage.SearchAirports(search);

            if (!matchingAirports.Any())
            {
                return NotFound();
            }

            return Ok(matchingAirports);
        }
        /*
        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights([FromBody] SearchFlightsRequest req)
        {
            var matchingFlights = FlightStorage.SearchFlights(req);
            return Ok(matchingFlights);
        }
        */
        /*
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var flight = FlightStorage.FindFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
        */
    }
}
