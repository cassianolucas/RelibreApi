using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class AuthorRepository : IAuthor
    {
        private readonly RelibreContext _context;

        public AuthorRepository(
            RelibreContext context
        )
        {
            _context = context;
        }
        public Task CreateAsync(Author model)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Author>> GetAllAsync()
        {
            return _context.Author
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Author> GetByIdAsync(long Id)
        {
            return _context.Author
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }

        public Task<Author> GetByIdAsyncNoTracking(long Id)
        {
            return _context.Author
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public Task<Author> GetByName(string name)
        {
            return _context.Author
                .Where(x => x.Name.ToLower().Trim()
                    .Equals(name.ToLower().Trim()))
                .FirstOrDefaultAsync();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Author> UpdateAsync(Author model)
        {
            throw new System.NotImplementedException();
        }
    }
}