using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class LibraryRepository : ILibrary
    {
        private readonly RelibreContext _context;

        public LibraryRepository(
            RelibreContext context
            )
        {
            _context = context;
        }
        public Task CreateAsync(Library model)
        {

            return Task.Run(() => _context.Library.AddAsync(model));
        }

        public Task<List<Library>> GetAllAsync()
        {
            return _context.Library
                .Include(x => x.Person)
                .Include(x => x.LibraryBooks)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Library> GetByIdAsync(long Id)
        {
            return _context.Library
                .Include(x => x.Person)
                .Include(x => x.LibraryBooks)
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }

        public Task<Library> GetByIdAsyncNoTracking(long Id)
        {
            return _context.Library
                .Include(x => x.Person)
                .Include(x => x.LibraryBooks)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Library model)
        {
            throw new System.NotImplementedException();
        }
    }
}