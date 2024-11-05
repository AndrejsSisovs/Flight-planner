using AutoMapper;
using Flightplanner.Services;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IEnumerable<IValidator> _validators;
        private readonly IValidator<Flight> _validator;
        private readonly IMapper _mapper;

        public CustomerController(
            IFlightService flightService, 
            IAirportService airportService,
            IMapper mapper,
            IEnumerable<IValidator> validators,
            IValidator<Flight> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _airportService = airportService;
            _validators = validators;
            _validator = validator;
        }

        [Route("airports")]
        [HttpGet]
        public IActionResult SearchAirports(string search)
        {
            var result = _airportService.SearchAirports(search);

            if (!result.Any())
            {
                return NotFound();
            }

            var response = _mapper.Map<IEnumerable<AirportResponse>>(result);

            return Ok(response);
        }


        [Route("flights/search")]
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsRequest searchRequest)
        {

            var matchingFlights = _flightService.SearchFlights(searchRequest);

            if (!matchingFlights.Any())
            {
                return Ok(new { Page = 0, TotalItems = 0, Items = new List<Flight>() });
            }

            return Ok(new { Page = 0, TotalItems = matchingFlights.Count(), Items = matchingFlights });
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var result = _flightService.GetFullFlightById(id);
            if (result == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<FlightResponse>(result); ;
            return Ok(response);
        }


    }
}
