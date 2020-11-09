using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class ProfileRepository : IProfile
    {
        private readonly RelibreContext _context;

        public ProfileRepository(
            RelibreContext context
            )
        {
            _context = context;
        }
        public Task CreateAsync(Profile model)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Profile>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Profile> GetByIdAsync(long Id)
        {
            return _context.Profile
                .Include(x => x.AccessProfiles)
                .Where(x => x.Id == Id)
                .SingleAsync();
        }

        public Task<Profile> GetByIdAsyncNoTracking(long Id)
        {
            return _context.Profile
                .Include(x => x.AccessProfiles)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .SingleAsync();
        }

        public void RemoveAsync(Profile model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Profile model)
        {
            throw new System.NotImplementedException();
        }
    }
}