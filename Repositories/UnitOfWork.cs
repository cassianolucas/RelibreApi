using RelibreApi.Data;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RelibreContext _context;

        public UnitOfWork(RelibreContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void RollBack()
        {
            //
        }
    }
}