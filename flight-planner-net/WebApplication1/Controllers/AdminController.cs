using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IValidator<Flight> _validator;
        private readonly IMapper _mapper;
        private static readonly object _flightLock = new object();

        public AdminController(
            IFlightService flightService,
            IMapper mapper,
            IValidator<Flight> validator)
        {
            _flightService = flightService;
            _validator = validator;
            _mapper = mapper;
        }


        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var result = _flightService.GetFullFlightById(id);
            if (result == null)
            {

                return NotFound();
            }
            var response = _mapper.Map<FlightResponse>(result); ;
            return Ok(response);
        }
        

        [Route("flights")]
        [HttpPost]
        public IActionResult AddFlight(FlightRequest request)
        {
            lock (_flightLock)
            {
                var flight = _mapper.Map<Flight>(request);
                var validationResult = _validator.Validate(flight);

                if (!validationResult.IsValid)
                {
                    return BadRequest();
                }

                if (_flightService.FlightExists(flight))
                {
                    return Conflict();
                }

                var result = _flightService.Create(flight);
                var response = _mapper.Map<FlightResponse>(flight);
                response.Id = result.Entity.Id;

                return Created("", response);
            }
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            lock (_flightLock) 
            {
                var delete = _flightService.GetById(id);

                if (delete != null)
                {
                    var result = _flightService.Delete(delete);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }

                return Ok();
            }
        }
    }
}
