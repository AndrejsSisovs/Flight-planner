using AutoMapper;
using Azure.Core;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;
using IValidator = WebApplication1.Validations.IValidator;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IEnumerable<IValidator> _validators;
        private readonly IValidator<Flight> _validator;
        private readonly IMapper _mapper;

        public AdminController(
            IFlightService flightService,
            IEnumerable<IValidator> validators,
            IMapper mapper,
            IValidator<Flight> validator)
        {
            _flightService = flightService;
            _validators = validators;
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

            var flight = _mapper.Map<Flight>(request);
            var validationResult = _validator.Validate(flight);
            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            if (_flightService.FlightExists(flight))
            {
                return Conflict("This flight already exists.");
            }

            var result =  _flightService.Create(flight);
            var response = _mapper.Map<FlightResponse>(flight);
            response.Id = result.Entity.Id;
            return Created("", response);
        }


        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            var result = _flightService.DeleteFlight(id);
            
            return Ok();
        }
    }
}
