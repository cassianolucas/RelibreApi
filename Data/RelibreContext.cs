using Microsoft.EntityFrameworkCore;
using RelibreApi.Maps;
using RelibreApi.Models;

namespace RelibreApi.Data
{
    public class RelibreContext: DbContext
    {
        public DbSet<Person> Person { get; set; }
        public RelibreContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaMap());
            
        }
    }
}