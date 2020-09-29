using System.Collections.Generic;
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

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Contact model)
        {
            throw new System.NotImplementedException();
        }
    }
}