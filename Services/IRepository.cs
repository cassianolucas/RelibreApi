using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelibreApi.Services
{
    public interface IRepository<T> where T : class
    {                
        Task CreateAsync(T model);
        void Update(T model);
        void RemoveAsync(T model);
        Task<T> GetByIdAsync(long Id);
        Task<T> GetByIdAsyncNoTracking(long Id);
        Task<List<T>> GetAllAsync();
    }
}