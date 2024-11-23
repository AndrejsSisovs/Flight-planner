using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace Flightplanner.Services
{
    public class Dbservice : IDbService
    {
        protected FlightPlannerDbContext _context;

        public Dbservice (FlightPlannerDbContext context)
        {
            _context = context;
        }
        public ServiceResult Create<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return new ServiceResult(true).Set(entity);
        }

        public ServiceResult Delete<T>(T entity) where T : Entity
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();

            return new ServiceResult(true);
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault<T>(entity => entity.Id == id);
        }

        public IEnumerable<T> List<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public ServiceResult Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

            return new ServiceResult(true).Set(entity);
        }
    }
}
