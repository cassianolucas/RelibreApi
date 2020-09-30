using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface ILibrary : IRepository<Library>
    {
        Task<Library> GetLibraryByPerson(long idPerson);
    }
}