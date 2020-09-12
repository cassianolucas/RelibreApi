using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class UserRepository : IUser
    {
        private readonly RelibreContext _context;

        public UserRepository(
            RelibreContext context
            )
        {
            _context = context;
        }
        public Task CreateAsync(User model)
        {                        
            return Task.Run(() => _context.User.AddAsync(model));
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetByIdAsync(long Id)
        {
            return _context.User
                .Include(x => x.Person)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x => x.IdPerson == Id)
                .SingleOrDefaultAsync();
        }

        public Task<User> GetByIdAsyncNoTracking(long Id)
        {
            return _context.User
                .Include(x => x.Person)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x => x.IdPerson == Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public Task<User> GetByLogin(string login)
        {
            return _context.User
                .Include(x => x.Person)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x => x.Login.ToLower().Trim().Equals(login.ToLower().Trim()))
                .SingleOrDefaultAsync();
        }

        public Task<User> GetByLoginOrDocument(string login, string document)
        {
            return _context.User
                .Include(x => x.Person)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x => x.Login.ToLower().Trim().Equals(login.ToLower().Trim()) || 
                    x.Person.Document.Trim().Equals(document.Trim()))
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public Task<User> LoginAsync(string login, string password)
        {
            return _context.User
                .Include(x => x.Person)
                    .ThenInclude(x => x.Phones)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x => x.Login.ToLower().Trim().Equals(login) && x.Password.Equals(password))
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(User model)
        {
            throw new System.NotImplementedException();
        }
    }
}