using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        //[Route("airports")]
        //[HttpGet]
        //public IActionResult SearchAirports(string search)
        //{
        //    if (string.IsNullOrEmpty(search))
        //    {
        //        return BadRequest();
        //    }

        //    var matchingAirports = _airportStorage.SearchAirports(search);

        //    if (!matchingAirports.Any())
        //    {
        //        return NotFound();
        //    }

        //    return Ok(matchingAirports);
        //}

        ////[Route("flights/search")]
        ////[HttpPost]
        ////public IActionResult SearchFlights(SearchFlightsRequest searchRequest)
        ////{
        ////    if (string.IsNullOrEmpty(searchRequest.DepartureAirport) ||
        ////        string.IsNullOrEmpty(searchRequest.DestinationAirport) ||
        ////        searchRequest.FlightDate == DateTime.MinValue ||
        ////        searchRequest.DepartureAirport == searchRequest.DestinationAirport)
        ////    {
        ////        return BadRequest();
        ////    }

        ////    var matchingFlights = _flightStorage.SearchFlights(searchRequest);

        ////    if (matchingFlights.Count() == 0)
        ////    {
        ////        return Ok(new { Page = 0, TotalItems = 0, Items = matchingFlights.Any() ? matchingFlights : new List<Flight>() });
        ////    }

        ////    return Ok(new { Page = 0, TotalItems = matchingFlights.Count(), Items = new List<Flight>() });
        ////}

        //[Route("flights/{id}")]
        //[HttpGet]
        //public IActionResult FindFlightById(int id)
        //{
        //    var flight = _flightStorage.FindFlightById(id);

        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(flight);
        //}
    }
}
