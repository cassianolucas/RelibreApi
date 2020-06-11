using System.Collections.Generic;
using System.Threading.Tasks;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class UserRepository : IUser
    {
        public Task<User> CreateAsync(User model)
        {
            // validar se já existe usuario no banco com ask no traking
            // caso exista rentornar exeção
            // se não existir cadastrar e retornar objeto


            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public User GetByIdAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public User GetByIdAsyncNoTracking(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> LoginAsync(string user, string password)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAsync(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(User model)
        {
            throw new System.NotImplementedException();
        }
    }
}