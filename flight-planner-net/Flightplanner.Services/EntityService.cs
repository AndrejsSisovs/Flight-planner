using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flightplanner.Services
{
    public class EntityService<T> : Dbservice, IEntityService<T> where T : Entity
    {
        public EntityService(FlightPlannerDbContext context) : base(context)
        {
        }

        public ServiceResult Create(T entity)
        {
            return Create<T>(entity);
        }

        public ServiceResult Delete(T entity)
        {
            return Delete<T>(entity);
        }

        public T? GetById(int id)
        {
            return GetById<T>(id);
        }

        public IEnumerable<T> List()
        {
            return List<T>();
        }

        public ServiceResult Update(T entity)
        {
            return Update<T>(entity);
        }
    }
}
