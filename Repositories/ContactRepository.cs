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
    public class ContactRepository : IContact
    {
        private readonly RelibreContext _context;

        public ContactRepository(
            RelibreContext context
            )
        {
            _context = context;
        }
        public Task CreateAsync(Contact model)
        {
            return Task.Run(() => _context.AddAsync(model));
        }

        public Task<List<Contact>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Contact> GetByEmail(string email)
        {
            return _context.Contact
                .Include(x => x.ContactBooksOwner)
                    .ThenInclude(x => x.LibraryBook)
                        .ThenInclude(x => x.Library)
                            .ThenInclude(x => x.Person)
                .Include(x => x.ContactBooksRequest)
                    .ThenInclude(x => x.LibraryBook)
                            .ThenInclude(x => x.Library)
                                .ThenInclude(x => x.Person)
                .SingleOrDefaultAsync(x =>
                    x.Email.ToLower()
                    .Equals(email.ToLower()));
        }

        public Task<Contact> GetByIdAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Contact> GetByIdAsyncNoTracking(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ContactBook> GetByOwner(long idLiraryBook, long idContactOwner, long idContactRequest)
        {
            return _context.ContactBook
                .Include(x => x.ContactRequest)
                .Include(x => x.LibraryBook)
                .Include(x => x.LibraryBook.Library)
                .Include(x => x.LibraryBook.Library.Person)
                .Include(x => x.LibraryBook.Book)
                .Include(x => x.LibraryBook.Book.AuthorBooks)
                .Include(x => x.LibraryBook.Book.CategoryBooks)
                .Where(x => x.ContactOwner.Id == idContactOwner &&
                    x.ContactRequest.Id == idContactRequest &&
                    x.LibraryBook.Id == idLiraryBook &&
                    x.Approved == false && x.Denied == false)
                .SingleOrDefaultAsync();
        }

        public Task<List<ContactBook>> GetByOwnerNoTracking(string email, bool approved, bool denied, int limit, int offset)
        {
            return _context.ContactBook
                .Include(x => x.ContactRequest)
                .Include(x => x.LibraryBook)
                .Include(x => x.LibraryBook.Library)
                .Include(x => x.LibraryBook.Library.Person)
                .Include(x => x.LibraryBook.Book)
                .Include(x => x.LibraryBook.Book.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Include(x => x.LibraryBook.Book.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Where(x => x.ContactOwner.Email.Equals(email) &&
                    x.Approved == approved &&
                    x.Denied == denied)
                .AsNoTracking()
                .Take((limit > 0 ? limit : 30))
                .Skip((offset > 0 ? offset : 0))
                .ToListAsync();
        }

        public Task<List<ContactBook>> GetByRequestNoTracking(string email, bool approved, bool denied, int limit, int offset)
        {
            return _context.ContactBook
                .Include(x => x.ContactOwner)
                .Include(x => x.LibraryBook)
                .Include(x => x.LibraryBook.Library)
                .Include(x => x.LibraryBook.Library.Person)
                .Include(x => x.LibraryBook.Book)
                .Include(x => x.LibraryBook.Book.AuthorBooks)
                .Include(x => x.LibraryBook.Book.CategoryBooks)
                .Where(x => x.ContactRequest.Email.Equals(email) &&
                    x.Approved == approved && x.Denied == denied)
                .AsNoTracking()
                .Take((limit > 0 ? limit : 30))
                .Skip((offset > 0 ? offset : 0))
                .ToListAsync();
        }

        public long GetQuantityConcactReceivedNoTracking(string email, DateTime current)
        {
            return _context.ContactBook
                .Include(x => x.ContactRequest)
                .Include(x => x.LibraryBook)
                .Include(x => x.LibraryBook.Library)
                .Include(x => x.LibraryBook.Library.Person)
                .Include(x => x.LibraryBook.Book)
                .Include(x => x.LibraryBook.Book.AuthorBooks)
                    .ThenInclude(x => x.Author)
                .Include(x => x.LibraryBook.Book.CategoryBooks)
                    .ThenInclude(x => x.Category)
                .Where(x => x.ContactOwner.Email.Equals(email) && 
                    (current.ToString("MM/dd/yyyy").Equals("01/01/1900") || 
                        x.CreatedAt >= current ))
                .AsNoTracking()                
                .Count();
        }

        public void RemoveAsync(Contact model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Contact model)
        {
            _context.Contact.Update(model);
        }

        public void UpdateContactBook(ContactBook contactBook)
        {
            _context.ContactBook.Update(contactBook);
        }
    }
}