using FindMyLocation.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FindMyLocation.Persistence;

namespace FindMyLocation.Service.Implementation
{
    public class RepositoryService<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;

        public RepositoryService(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAll()
        {
            return _entities.AsEnumerable();
        }
        /*async Task<IAsyncEnumerable<T>> IRepository<T>.GetAllAsync()
        {
            return entities.AsAsyncEnumerable();
        }*/

        async Task<T> IRepository<T>.Get(int id)
        {
            //return entities.SingleOrDefault(s => s.Id == id);
            return null;
        }

        public async Task<T> GetBy(object entity)
        {
            return _entities.Find(entity);
        }

        void IRepository<T>.Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            _context.Entry(entity).State = EntityState.Added;
        }

        void IRepository<T>.Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        void IRepository<T>.Delete(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            T existing = _entities.Find(entity);
            //context.Entry(entity).State = EntityState.Modified;
            _entities.Remove(existing);
        }

        void IRepository<T>.Remove(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Remove(entity);
        }

        void IRepository<T>.SaveChanges()
        {
            _context.SaveChanges();
        }
        async Task IRepository<T>.SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
