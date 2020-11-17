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
                new Type { Id = 1, Description = "Trocar", CreatedAt = Util.CurrentDateTime() },
                new Type { Id = 2, Description = "Doar", CreatedAt = Util.CurrentDateTime() },
                new Type { Id = 3, Description = "Emprestar", CreatedAt = Util.CurrentDateTime() },
                new Type { Id = 4, Description = "Interesse", CreatedAt = Util.CurrentDateTime() },
                new Type { Id = 5, Description = "Venda", CreatedAt = Util.CurrentDateTime() }
            };

            modelBuilder.Entity<Type>().HasData(types);

            var plans = new List<Subscription>
            {
                new Subscription { Id = 1, Description = "Pacote de 1 mês", Period = 1, Value = 15.00, CreatedAt = Util.CurrentDateTime()  },
                new Subscription { Id = 2, Description = "Pacote de 3 meses", Period = 3, Value = 39.00, CreatedAt = Util.CurrentDateTime()  },
                new Subscription { Id = 3, Description = "Pacote de 6 meses", Period = 6, Value = 60.00, CreatedAt = Util.CurrentDateTime()  }
            };

            modelBuilder.Entity<Subscription>().HasData(plans);
            
        }
    }
}