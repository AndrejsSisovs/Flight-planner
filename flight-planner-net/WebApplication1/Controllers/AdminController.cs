using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Storage;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController(IEntityService<Flight> flightService) : ControllerBase
    {
        private readonly IEntityService<Flight> _flightService = flightService;

        private readonly FlightStorage _flightStorage;
        private readonly AirportStorage _airportStorage;

        //public AdminController(FlightStorage flightStorage, AirportStorage airportStorage)
        //{
        //    _flightStorage = flightStorage;
        //    _airportStorage = airportStorage;
        //}
        
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var result = _flightService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("flights")]
        [HttpPost]
        public IActionResult AddFlight(Flight flight)
        {
            //if (flight == null)
            //{
            //    return BadRequest();
            //}

            //if (_flightStorage.FlightIsNull(flight))
            //{
            //    return BadRequest();
            //}

            //if (_flightStorage.FlightExists(flight))
            //{
            //    return Conflict();
            //}

            //if (_flightStorage.FlightStringComparision(flight))
            //{
            //    return BadRequest();
            //}

            //if (_flightStorage.AreFlightDatesInvalid(flight))
            //{
            //    return BadRequest();
            //}

            //_airportStorage.AddAirport(flight.From);
            //_airportStorage.AddAirport(flight.To);

            //if (flight == null)
            //{
            //    return Conflict();
            //}

            //_flightStorage.AddFlight(flight);


            var result =  _flightService.Create(flight);


            return Created("", flight);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            bool isDeleted = _flightStorage.FlightDeleted(id);

            if (isDeleted)
            {
                return Ok();
            }

            return Ok();
        }
    }
}
