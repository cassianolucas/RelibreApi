using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface INotification : IRepository<Notification>
    {
        Task<List<NotificationPerson>> GetByPersonAsyncNoTracking(long idPerson, int offset, int limit);
    }
}