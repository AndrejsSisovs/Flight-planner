using AutoMapper;
using Azure.Core;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;
using WebApplication1.Storage;
using WebApplication1.Validations;
using IValidator = WebApplication1.Validations.IValidator;

namespace WebApplication1.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize] 
    public class AdminController(
        IFlightService flightService, 
        IEnumerable<IValidator> validators,
        IMapper mapper,
        IValidator<Flight> validator) : ControllerBase
    {
        private readonly IFlightService _flightService = flightService;
        private readonly IEnumerable<IValidator> _validators = validators;
        private readonly IValidator<Flight> _validator = validator;
        private readonly IMapper _mapper = mapper;

        
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
            return Ok(result);
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

            var result =  _flightService.Create(flight);
            var response = _mapper.Map<FlightResponse>(flight);
            response.Id = result.Entity.Id;
            return Created("", flight);
        }


        //[Route("flights/{id}")]
        //[HttpDelete]
        //public IActionResult DeleteFlight(int id)
        //{
        //    bool isDeleted = _flightStorage.FlightDeleted(id);

        //    if (isDeleted)
        //    {
        //        return Ok();
        //    }

        //    return Ok();
        //}
    }
}
