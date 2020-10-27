using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;

namespace RelibreApi.Services
{
    public interface IContact : IRepository<Contact>
    {
        Task<Contact> GetByEmail(string email);
        Task<List<ContactBook>> GetByOwnerNoTracking(string email, bool approved, bool denied, int limit, int offset);
        Task<List<ContactBook>> GetByRequestNoTracking(string email, bool approved, bool denied, int limit, int offset);
        Task<ContactBook> GetByOwner(long idLiraryBook, long idContact, long idContactRequest);
        void UpdateContactBook(ContactBook contactBook);
    }
}