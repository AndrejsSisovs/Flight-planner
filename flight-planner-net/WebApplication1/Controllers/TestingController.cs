using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Storage;

namespace WebApplication1.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController(IDbClearingService dbClearingService) : ControllerBase
    {
        private readonly IDbClearingService _dbClearingService = dbClearingService;
        //private readonly FlightStorage _storage;

        //public TestingController(FlightStorage storage) 
        //{
        //    _storage = storage;
        //}

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            // _storage.ClearFlights();

            _dbClearingService.Clear<Airport>();
            _dbClearingService.Clear<Flight>();
            return Ok();
        }
    }
}
