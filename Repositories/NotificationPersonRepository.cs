using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class NotificationPersonRepository : INotificationPerson
    {
        private readonly RelibreContext _context;
        public NotificationPersonRepository(
            RelibreContext context
        )
        {
            _context = context;
        }
        public Task CreateAsync(NotificationPerson model)
        {
            return Task.Run(() => _context
                .NotificationPerson.AddAsync(model));
        }

        public Task<List<NotificationPerson>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<NotificationPerson> GetByIdAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<NotificationPerson> GetByIdAsyncNoTracking(long Id)
        {
            throw new System.NotImplementedException();            
        }
        
        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(NotificationPerson model)
        {
            _context.NotificationPerson.Update(model);
        }
    }
}