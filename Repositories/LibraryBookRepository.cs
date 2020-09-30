using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;
using RelibreApi.Utils;

namespace RelibreApi.Repositories
{
    public class LibraryBookRepository : ILibraryBook
    {
        private readonly RelibreContext _context;

        public LibraryBookRepository(
            RelibreContext context
            )
        {
            _context = context;
        }
        public Task CreateAsync(LibraryBook model)
        {
            return Task.Run(() => 
                _context.LibraryBook.AddAsync(model));
        }

        public Task<List<LibraryBook>> GetAllAsync()
        {
            return _context.LibraryBook
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<LibraryBook>> GetByBookTitle(string title, int offset, int limit)
        {
            return _context.LibraryBook
                .Include(x => x.Book)
                    .ThenInclude(x => x.AuthorBooks)
                        .ThenInclude(x => x.Author)
                .Include(x => x.Book)
                    .ThenInclude(x => x.CategoryBooks)
                        .ThenInclude(x => x.Category)
                .Include(x => x.LibraryBookTypes)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Library)
                .Include(x => x.Images)
                .Include(x => x.Contact)
                .Where(x => x.Id >= 0 && string.IsNullOrEmpty(title) ||
                    !string.IsNullOrEmpty(title) && 
                    Util.RemoveSpecialCharacter(
                        x.Book.Title.ToLower())
                        .Contains(title.ToLower()))
                .AsNoTracking()
                .Take((limit > 0? limit: 30))
                .Skip((offset > 0? offset: 0))
                .ToListAsync();
        }

        public Task<LibraryBook> GetByIdAsync(long Id)
        {
            return _context.LibraryBook
                .Include(x => x.Book)
                .Include(x => x.LibraryBookTypes)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Library)
                .Include(x => x.Images)                
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }
        public Task<LibraryBook> GetByIdAsyncNoTracking(long Id)
        {
            return _context.LibraryBook
                .Include(x => x.Book)
                .Include(x => x.LibraryBookTypes)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Library)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }

        public Task<List<LibraryBook>> GetByIdLibrary(long IdLibrary, int offset, int limit)
        {
            return _context.LibraryBook
                .Include(x => x.Book)
                .Include(x => x.LibraryBookTypes)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Library)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => x.IdLibrary == IdLibrary)
                .Take(offset > 0? offset : 30)
                .Skip(limit > 0? limit : 0)
                .ToListAsync();
        }

        public Task<List<LibraryBook>> GetByTypeNoTracking(Type type, int offset, int limit)
        {
            return _context.LibraryBook
                .Include(x => x.Book)
                .Include(x => x.Book.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Include(x => x.Book.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Include(x => x.LibraryBookTypes)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Library)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => x.LibraryBookTypes.Any(x => x.IdType == type.Id))
                .Take(offset > 0? offset : 30)
                .Skip(limit > 0? limit : 0)
                .ToListAsync();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(LibraryBook model)
        {
            throw new System.NotImplementedException();
        }
    }
}