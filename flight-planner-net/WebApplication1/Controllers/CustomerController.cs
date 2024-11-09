using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IValidator<SearchFlightsRequest> _validator;

        public CustomerController(
            IFlightService flightService, 
            IAirportService airportService,
            IMapper mapper,
            IValidator<SearchFlightsRequest> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _airportService = airportService;
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
            var validationResult = _validator.Validate(searchRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            var mappedSearchRequest = new UserSearchFlights
            {
                From = searchRequest.From,
                To = searchRequest.To,
                DepartureDate = searchRequest.DepartureDate
            };


            var matchingFlights = _flightService.SearchFlights(mappedSearchRequest);

            if (!matchingFlights.Any())
            {
                return Ok(new { page = 0, totalItems = 0, items = new List<Flight>() });
            }

            return Ok(new
            {
                page = 0,
                totalItems = matchingFlights.Count(),
                items = matchingFlights
            });
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

            var response = _mapper.Map<FlightResponse>(result);

            return Ok(response);
        }
    }
}
