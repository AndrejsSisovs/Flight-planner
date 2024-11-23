using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace Flightplanner.Services
{
    public class DbClearingService : Dbservice, IDbClearingService
    {
        public DbClearingService(FlightPlannerDbContext context) : base(context)
        {
        }

        public ServiceResult Clear<T>() where T : Entity
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();

            return new ServiceResult(true);
        }
    }
}
