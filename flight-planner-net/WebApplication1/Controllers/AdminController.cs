﻿using Microsoft.AspNetCore.Authorization;
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
                return BadRequest("Flight object is null");
            }

            if (FlightStorage.FlightIsNull(flight))
            {
                return BadRequest("Invalid flight data");
            }

            if (FlightStorage.FlightExists(flight))
            {
                return Conflict("Flight already exists");
            }

            if (FlightStorage.FlightStringComparision(flight))
            {
                return BadRequest("Departure and arrival airports cannot be the same.");
            }


            Flight storedFlight = FlightStorage.AddFlight(flight);

            return Created("", storedFlight);
        }
    }
}
