

using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IType : IRepository<Type>
    {
         Task<Type> GetByDescriptionAsync(string description);
    }
}