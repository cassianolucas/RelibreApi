using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface ILibraryBook : IRepository<LibraryBook>
    {
         Task<List<LibraryBook>> GetByIdLibrary(long IdLibrary, int offset, int limit);

         Task<List<LibraryBook>> GetByBookTitle(string title, int offset, int limit);         
    }
}