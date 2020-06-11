using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IUser : IRepository<User>
    {
        Task<User> LoginAsync(string user, string password);
    }
}