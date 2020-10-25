using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface ILibraryBook : IRepository<LibraryBook>
    {
         Task<List<LibraryBook>> GetByIdLibrary(long IdLibrary, int offset, int limit);

         Task<List<LibraryBook>> GetByBookTitle(string title, long idLibraryRequest, int offset, int limit);

         Task<List<LibraryBook>> GetByTypeNoTracking(Type type, long idLibraryRequest, int offset, int limit);
         Task<List<LibraryBook>> GetByAssociated(string category);
         Task<List<LibraryBook>> GetByBusiness(int offset, int limit);
    }
}