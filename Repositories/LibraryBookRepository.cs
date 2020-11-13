using System;
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
                .Include(x => x.Book)
                .Include(x => x.Book.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Include(x => x.Book.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Include(x => x.LibraryBookTypes)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Library)
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .AsNoTracking()
                .ToListAsync();
        }
        public Task<List<LibraryBook>> GetByTitleAndTypeNoTracking(string title, Models.Type type, int offset, int limit)
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
                .Include(x => x.Library.Person)                
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .Where(x => x.LibraryBookTypes.Any(x => x.IdType == type.Id) && 
                    (string.IsNullOrEmpty(title) || (!string.IsNullOrEmpty(title) 
                        && x.Book.Title.ToLower().Contains(title.ToLower()))))
                .AsNoTracking()
                .Take((limit > 0? limit: 30))
                .Skip((offset > 0? offset: 0))
                .ToListAsync();
        }
        public Task<LibraryBook> GetByIdAsync(long Id)
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }
        public Task<LibraryBook> GetByIdAsyncNoTracking(long Id)
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => x.Id == Id)
                .SingleOrDefaultAsync();
        }
        public Task<List<LibraryBook>> GetByIdLibrary(long IdLibrary, string title, int offset, int limit)
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .Where(x => x.IdLibrary == IdLibrary && 
                    (string.IsNullOrEmpty(title) || (!string.IsNullOrEmpty(title) && 
                        x.Book.Title.ToLower().Contains(title.ToLower()))) && 
                            x.LibraryBookTypes.Any(x => x.IdType != 4))
                .Take(limit > 0? limit : 30)
                .Skip(offset > 0? offset : 0)
                .ToListAsync();
        }
        public Task<List<LibraryBook>> GetByTypeNoTracking(Models.Type type, long idLibraryRequest, string title, int offset, int limit)
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => (type != null && 
                    x.LibraryBookTypes.Any(x => x.IdType == type.Id)) && 
                        x.IdLibrary == idLibraryRequest && 
                            (string.IsNullOrEmpty(title) || (!string.IsNullOrEmpty(title) && 
                                x.Book.Title.ToLower().Contains(title.ToLower()))))
                .Take(offset > 0? offset : 30)
                .Skip(limit > 0? limit : 0)
                .ToListAsync();
        }
        public Task<List<LibraryBook>> GetByAssociatedNoTracking(string category, Models.Type type, long idLibraryRequest)
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => x.Book.CategoryBooks
                    .Any(x => x.Category.Name.Equals(category)) && 
                        x.LibraryBookTypes.Any(x => x.Type.Id == type.Id) && 
                            x.IdLibrary != idLibraryRequest)
                .Distinct()
                .ToListAsync();
        }
        public Task<List<LibraryBook>> GetByBusinessNoTracking(int offset, int limit)
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .Where(x => x.Library.Person.PersonType.Equals("PJ"))
                .AsNoTracking()
                .Take(offset > 0? offset : 30)
                .Skip(limit > 0? limit : 0)
                .Distinct()
                .ToListAsync();
        }
        public void RemoveAsync(LibraryBook model)
        {            
            _context.LibraryBook.Remove(model);
        }
        public void Update(LibraryBook model)
        {
            _context.LibraryBook.Update(model);
        }
        public Task<List<LibraryBook>> GetAll()
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .Where(x => x.Library.Person.PersonType.Equals("PF"))
                .ToListAsync();
        }
        public Task<List<LibraryBook>> GetByTypeOnAllLibraryNoTracking(Models.Type type, long idLibraryRequest, string title, int offset, int limit)
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
                .Include(x => x.Library.Person)
                .Include(x => x.Library.Person.Addresses)
                .Include(x => x.Images)
                .AsNoTracking()
                .Where(x => (type != null && 
                    x.LibraryBookTypes.Any(x => x.IdType == type.Id)) && 
                        x.IdLibrary != idLibraryRequest && 
                            (string.IsNullOrEmpty(title) || (!string.IsNullOrEmpty(title) && 
                                x.Book.Title.ToLower().Contains(title.ToLower()))))
                .Take(offset > 0? offset : 30)
                .Skip(limit > 0? limit : 0)
                .ToListAsync();
        }
    }
}