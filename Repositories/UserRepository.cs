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

        public Address GetAddressByLogin(string login)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetAllBusiness(long idPerson)
        {
            return _context.User
                .Include(x => x.Person)
                .Include(x => x.Person.Phones)
                .Include(x => x.Person.Addresses)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x => x.IdPerson != idPerson)
                .ToListAsync();
        }

        public Task<User> GetByIdAsync(long Id)
        {
            return _context.User
                .Include(x => x.Person.Phones)
                .Include(x => x.Person.Addresses)
                .Include(x => x.Profile)                
                // .Include(x => x.Person.Library)
                .Include(x => x.Person)
                .Where(x => x.Person.Id == Id)
                .SingleOrDefaultAsync();
        }

        public Task<User> GetByIdAsyncNoTracking(long Id)
        {
            return _context.User
                .Include(x => x.Person)
                .Include(x => x.Person.Phones)
                .Include(x => x.Person.Addresses)
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
                .Include(x => x.Person.Phones)
                .Include(x => x.Person.Addresses)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x => x.Login.ToLower().Trim().Equals(login.ToLower().Trim()))
                .SingleOrDefaultAsync();
        }

        public Task<User> GetByLoginOrDocumentNoTracking(string login, string document)
        {
            return _context.User
                .Include(x => x.Person)
                .Include(x => x.Person.Phones)
                .Include(x => x.Person.Addresses)
                .Include(x => x.Profile)
                .Include(x => x.Person.Library)
                .Where(x =>
                    x.Login.ToLower().Trim().Equals(login.ToLower().Trim()) ||
                    x.Person.Document.Trim().Equals(document.Trim()))
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public double GetRatingByLogin(string login)
        {
            if (!string.IsNullOrEmpty(login))
            {
                var user = GetByLogin(login).Result;

                if (user.TotalCount > 0 && user.TotalValue > 0)
                {
                    return (user.TotalValue / user.TotalCount);
                }
            }

            return 0;
        }

        public void RemoveAsync(User model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User model)
        {
            _context.User.Update(model);
        }
    }
}