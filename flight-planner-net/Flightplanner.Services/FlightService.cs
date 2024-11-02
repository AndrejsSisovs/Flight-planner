using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flightplanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(FlightPlannerDbContext context) : base(context)
        {
        }

        public Flight? GetFullFlightById (int id)
        {
           return _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .SingleOrDefault(flight => flight.Id == id);
        }
    }
}
