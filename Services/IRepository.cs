using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelibreApi.Services
{
    public interface IRepository<T> where T : class
    {                
        Task CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        void RemoveAsync(long Id);
        Task<T> GetByIdAsync(long Id);
        Task<T> GetByIdAsyncNoTracking(long Id);
        Task<List<T>> GetAllAsync();
    }
}