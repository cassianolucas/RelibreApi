using System.Collections.Generic;
using System.Threading.Tasks;

namespace RelibreApi.Services
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T model);
        Task<T> UpdateAsync(T model);
        void RemoveAsync(long Id);
        T GetByIdAsync(long Id);
        T GetByIdAsyncNoTracking(long Id);
        IEnumerable<T> GetAllAsync();
    }
}