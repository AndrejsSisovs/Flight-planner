using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;
using WebApplication1.Storage;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            return NotFound();
        }

        [Route("flights")]
        [HttpPost]
        public IActionResult AddFlight(Flight flight)
        {
            if (flight == null)
            {
                return BadRequest();
            }

            if (FlightStorage.FlightIsNull(flight))
            {
                return BadRequest();
            }

            if (FlightStorage.FlightExists(flight))
            {
                return Conflict();
            }

            if (FlightStorage.FlightStringComparision(flight))
            {
                return BadRequest();
            }

            if (FlightStorage.AreFlightDatesInvalid(flight))
            {
                return BadRequest();
            }

            AirportStorage.AddAirport(flight.From);
            AirportStorage.AddAirport(flight.To);

            Flight storedFlight = FlightStorage.AddFlight(flight);

            if (storedFlight == null)
            {
                return Conflict();
            }

            return Created("", storedFlight);
        }


        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            bool isDeleted = FlightStorage.FlightDeleted(id);

            if (isDeleted)
            {
                return Ok();
            }

            return Ok();
        }
    }
}
