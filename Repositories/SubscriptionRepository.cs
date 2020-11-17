using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class SubscriptionRepository : ISubscription
    {
        private readonly RelibreContext _context;
        public SubscriptionRepository(
            RelibreContext context
        )
        {
            _context = context;
        }
        public Task CreateAsync(PersonSubscription model)
        {
            return Task.Run(() => 
                _context.PersonSubscription.Add(model));
        }

        public Task<List<PersonSubscription>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PersonSubscription> GetByIdAsync(long Id)
        {            
            throw new System.NotImplementedException();
        }

        public Task<PersonSubscription> GetByIdAsyncNoTracking(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<PersonSubscription> GetByPersonAsyncNoTacking(long idPerson)
        {
            return _context.PersonSubscription
                .Include(x => x.Subscription)
                .Where(x => x.Person.Id == idPerson)
                .SingleOrDefaultAsync();
        }

        public Task<Subscription> GetSubscriptionById(long idSubscription)
        {
            return _context.Subscription
                .Where(x => x.Id == idSubscription)
                .SingleOrDefaultAsync();
        }

        public void RemoveAsync(PersonSubscription model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(PersonSubscription model)
        {
            throw new System.NotImplementedException();
        }
    }
}