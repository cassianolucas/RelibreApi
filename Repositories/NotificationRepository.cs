using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class NotificationRepository : INotification
    {
        private readonly RelibreContext _context;
        public NotificationRepository(
            RelibreContext context
        )
        {
            _context = context;
        }
        public Task CreateAsync(Notification model)
        {
            return Task.Run(() => _context
                .Notification.AddAsync(model));
        }

        public Task<List<Notification>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Notification> GetByIdAsync(long Id)
        {
            return _context.Notification
                .Include(x => x.NotificationPeople)
                    .ThenInclude(x => x.Person)
                .Where(x => x.Id == Id)                
                .SingleOrDefaultAsync();
        }

        public Task<Notification> GetByIdAsyncNoTracking(long Id)
        {
            return _context.Notification
                .Include(x => x.NotificationPeople)
                    .ThenInclude(x => x.Person)
                .Where(x => x.Id == Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public Task<List<NotificationPerson>> GetByPersonAsyncNoTracking(long idPerson, int offset, int limit)
        {
            return _context.NotificationPerson
                .Include(x => x.Notification)
                .Include(x => x.Person)
                .Where(x => x.IdPerson == idPerson &&
                    x.Active == true)
                .AsNoTracking()
                .Take((limit > 0? limit: 30))
                .Skip((offset > 0? offset: 0))
                .ToListAsync();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Notification model)
        {
            _context.Notification.Update(model);
        }

        
    }
}