using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaatApiCore.Entities;

namespace SaatApiDataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SaatItem> Saatler => Set<SaatItem>();
        public DbSet<KayitAtama> Kayitlar => Set<KayitAtama>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SaatItem>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Saat).IsRequired().HasMaxLength(10);
            });

            modelBuilder.Entity<SaatItem>().HasData(
                new SaatItem { Id = 1, Saat = "08:00" },
                new SaatItem { Id = 2, Saat = "09:00" },
                new SaatItem { Id = 3, Saat = "10:00" },
                new SaatItem { Id = 4, Saat = "11:00" },
                new SaatItem { Id = 5, Saat = "12:00" }
            );


        }
    }

}
