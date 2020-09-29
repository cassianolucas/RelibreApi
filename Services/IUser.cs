using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IUser : IRepository<User>
    {        
        Task<User> GetByLoginOrDocument(string login, string document);
        Task<User> GetByLogin(string login);
    }
}