using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.DataAccess.Entites;

namespace VehicleRepairLog.DataAccess
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly VehicleProfileStorageContext context;
        private DbSet<T> entities;

        public Repository(VehicleProfileStorageContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T GetById(int id)
        {
            return entities.SingleOrDefault(x => x.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            context.SaveChanges();
        }
        public void Delete(int id)
        {
            T entity = entities.SingleOrDefault(x => x.Id == id);
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
