using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class CategoryRepository : ICategory
    {
        private readonly RelibreContext _context;

        public CategoryRepository(
            RelibreContext context
            )
        {
            _context = context;   
        }
        public Task CreateAsync(Category model)
        {
            return Task.Run(() => _context.Category.AddAsync(model));
        }

        public Task<List<Category>> GetAllAsync()
        {
            return _context.Category
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Category> GetByIdAsync(long Id)
        {
            return _context.Category
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }

        public Task<Category> GetByIdAsyncNoTracking(long Id)
        {
            return _context.Category
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public Task<Category> GetByName(string name)
        {
            return _context.Category
                .Where(x => x.Name.ToLower().Trim()
                    .Equals(name.ToLower().Trim()))
                .FirstOrDefaultAsync();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Category model)
        {
            throw new System.NotImplementedException();
        }
    }
}