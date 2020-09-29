using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RelibreApi.Models;
using RelibreApi.Utils;

namespace RelibreApi.Data
{
    public static class RelibreContextExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {            
            var profile = new List<Profile>
            {
                new Profile { Id = 1, Name = "PJ", Active = true, CreatedAt = Util.CurrentDateTime(), UpdatedAt = Util.CurrentDateTime() },
                new Profile { Id = 2, Name = "PF", Active = true, CreatedAt = Util.CurrentDateTime(), UpdatedAt = Util.CurrentDateTime() }
            };

            modelBuilder.Entity<Profile>().HasData(profile);

            var accessProfile = new List<AccessProfile>
            {
                new AccessProfile { Id = 1, Access = "access.administrator", IdProfile = 1 },
                new AccessProfile { Id = 2, Access = "access.default", IdProfile = 2 }
            };
            
            modelBuilder.Entity<AccessProfile>().HasData(accessProfile);

            var types = new List<Type>
            {                
                new Type { Id = 1, Description = "Troca", CreatedAt = Util.CurrentDateTime() },
                new Type { Id = 2, Description = "Doação", CreatedAt = Util.CurrentDateTime() },
                new Type { Id = 3, Description = "Emprestimo", CreatedAt = Util.CurrentDateTime() },
                new Type { Id = 4, Description = "Interesse", CreatedAt = Util.CurrentDateTime() }
            };

            modelBuilder.Entity<Type>().HasData(types);
        }
    }
}