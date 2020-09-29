using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class TypeRepository : IType
    {
        private readonly RelibreContext _context;

        public TypeRepository(
            RelibreContext context
            )
        {
            _context = context;
        }
        public Task CreateAsync(Type model)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Type>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }
        public Task<Type> GetByDescriptionAsync(string description)
        {
            return _context.Type
                .Where(x => x.Description
                    .ToLower().Equals(description.ToLower()))
                .SingleOrDefaultAsync();
        }

        public Task<Type> GetByIdAsync(long Id)
        {
            return _context.Type
                .Where(x => x.Id == Id)
                .SingleAsync();
        }

        public Task<Type> GetByIdAsyncNoTracking(long Id)
        {
            return _context.Type
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .SingleAsync();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Type model)
        {
            throw new System.NotImplementedException();
        }
    }
}