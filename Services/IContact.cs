using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IContact : IRepository<Contact>
    {
        Task<Contact> GetByEmail(string email);
        Task<List<ContactBook>> GetByOwnerNoTracking(string email, bool available, int limit, int offset);
        Task<List<ContactBook>> GetByRequestNoTracking(string email, bool available, int limit, int offset);        
    }
}