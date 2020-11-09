using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class BookRepository : IBook
    {
        private readonly RelibreContext _context;

        public BookRepository(
            RelibreContext context
            )
        {
            _context = context;
        }
        public Task CreateAsync(Book model)
        {
            return Task.Run(() => _context.Book.AddAsync(model));
        }

        public Task<List<Book>> GetAllAsync()
        {
            return _context.Book
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Book> GetByCodeIntegration(string codeIntegration)
        {
            return _context.Book
                .Include(x => x.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Include(x => x.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Where(x => x.CodeIntegration.Equals(codeIntegration))
                .SingleOrDefaultAsync();
        }

        public Task<Book> GetByIdAsync(long Id)
        {
            return _context.Book
                .Include(x => x.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Include(x => x.AuthorBooks)
                    .ThenInclude(x => x.Author)                
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }

        public Task<Book> GetByIdAsyncNoTracking(long Id)
        {
            return _context.Book
                .Include(x => x.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Include(x => x.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }        

        public Task<List<Book>> GetByTitleAsync(string title, int offset, int limit)
        {
            return _context.Book
                .Include(x => x.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Include(x => x.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Where(x => x.Id > 0 && string.IsNullOrEmpty(title) || 
                    !string.IsNullOrEmpty(title) && 
                        x.Title.ToLower().Trim().Contains(title.ToLower().Trim()))
                .AsNoTracking()
                .Take((limit > 0? limit: 30))
                .Skip((offset > 0? offset: 0))
                .ToListAsync();
        }

        public Task<Book> GetByTitleAsync(string title)
        {
            return _context.Book
                .Include(x => x.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Include(x => x.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Where(x => x.Title
                    .ToLower()
                    .Trim()
                    .Equals(title.
                        ToLower().Trim()))
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public void RemoveAsync(Book model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Book model)
        {
            throw new System.NotImplementedException();
        }
    }
}