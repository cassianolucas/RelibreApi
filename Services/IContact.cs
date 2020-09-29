using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IContact : IRepository<Contact>
    {
        Task<Contact> GetByEmail(string email);
    }
}