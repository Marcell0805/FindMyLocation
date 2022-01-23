using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMyLocation.Service.Contract
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        //Task<IAsyncEnumerable<T>> GetAllAsync();
        Task<T> Get(int id);
        Task<T> GetBy(object entity);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object entity);
        void Remove(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
