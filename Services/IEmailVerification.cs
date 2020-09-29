using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IEmailVerification
    {
         Task CreateAsync(EmailVerification model);

         Task<EmailVerification> GetByCodeAsync(string code);
    }
}