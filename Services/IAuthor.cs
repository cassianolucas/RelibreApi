using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IAuthor : IRepository<Author>
    {
        Task<Author> GetByName(string name);
    }
}