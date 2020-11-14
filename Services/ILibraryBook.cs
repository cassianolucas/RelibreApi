using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface ILibraryBook : IRepository<LibraryBook>
    {
         Task<List<LibraryBook>> GetByIdLibrary(long IdLibrary, string title, int offset, int limit);
         Task<List<LibraryBook>> GetByTitleAndTypeNoTracking(string title, Type type, int offset, int limit);
         Task<List<LibraryBook>> GetByTypeNoTracking(Type type, long idLibraryRequest, string title, int offset, int limit);
         Task<List<LibraryBook>> GetByTypeOnAllLibraryNoTracking(Type type, long idLibraryRequest, string title, int offset, int limit);
         Task<List<LibraryBook>> GetByAssociatedNoTracking(string category, Type type, long idLibraryRequest);
         Task<List<LibraryBook>> GetByBusinessNoTracking(int offset, int limit);
         Task<List<LibraryBook>> GetAll(long idLibraryRequest);
    }
}