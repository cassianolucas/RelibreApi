using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface ICategory : IRepository<Category>
    {
        Task<Category> GetByName(string name);
    }
}