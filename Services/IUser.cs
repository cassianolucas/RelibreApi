using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IUser : IRepository<User>
    {        
        Task<User> GetByLoginOrDocumentNoTracking(string login, string document);
        Task<User> GetByLogin(string login);
        double GetRatingByLogin(string login);        
        Task<List<User>> GetAllBusiness(long idPerson);
        Task<List<User>> GetAllBusinessNoTracking();
        Task<User> GetBusinessByPersonNoTracking(long idPerson);
    }
}