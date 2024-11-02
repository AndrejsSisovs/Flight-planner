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
        IValidator<Flight> _validator) : ControllerBase
    {
        private readonly IFlightService _flightService = flightService;
        private readonly IEnumerable<IValidator> _validators = validators;
        private readonly IValidator<Flight> _validator;
        private readonly IMapper _mapper = mapper;

        //private readonly FlightStorage _flightStorage;
        //private readonly AirportStorage _airportStorage;

        //public AdminController(FlightStorage flightStorage, AirportStorage airportStorage)
        //{
        //    _flightStorage = flightStorage;
        //    _airportStorage = airportStorage;
        //}
        
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
