using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface ILibraryBook : IRepository<LibraryBook>
    {
         Task<List<LibraryBook>> GetByIdLibrary(long IdLibrary, string title, int offset, int limit);
         Task<List<LibraryBook>> GetByTitleAndTypeNoTracking(string title, Models.Type type, int offset, int limit);
         Task<List<LibraryBook>> GetByTypeNoTracking(Type type, long idLibraryRequest, string title, int offset, int limit);
         Task<List<LibraryBook>> GetByAssociatedNoTracking(string category);
         Task<List<LibraryBook>> GetByBusinessNoTracking(int offset, int limit);
         Task<List<LibraryBook>> GetAll();
    }
}