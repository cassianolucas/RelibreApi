using System.Collections.Generic;

namespace RelibreApi.Services
{
    public interface IRepository<T> where T : class
    {
        void Add(T model);
        void Update(T model);
        void Remove(long Id);
        T GetById(long Id);
        T GetByIdNoTracking(long Id);
        IEnumerable<T> GetAll();
    }
}