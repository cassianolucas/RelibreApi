using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IBook : IRepository<Book>
    {
        Task<List<Book>> GetByTitleAsync(string title, int offset, int limit);
        Task<Book> GetByTitleAsync(string title);
    }
}