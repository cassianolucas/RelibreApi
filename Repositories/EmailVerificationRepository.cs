using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Data;
using RelibreApi.Models;
using RelibreApi.Services;

namespace RelibreApi.Repositories
{
    public class EmailVerificationRepository : IEmailVerification
    {
        private readonly RelibreContext _context;
        public EmailVerificationRepository(
            RelibreContext context
        )
        {
            _context = context;
        }

        public Task CreateAsync(EmailVerification model)
        {
            return Task.Run(() => 
                _context.EmailVerification.AddAsync(model));
        }
        public Task<EmailVerification> GetByCodeAsync(string code)
        {
            return _context.EmailVerification
                .Where(x => x.CodeVerification.Equals(code))
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}