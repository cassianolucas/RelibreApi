using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface ISubscription: IRepository<PersonSubscription>
    {
        Task<PersonSubscription> GetByPersonAsyncNoTacking(long idPerson);

        Task<Subscription> GetSubscriptionById(long idSubscription);
    }
}