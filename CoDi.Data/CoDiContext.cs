using CoDi.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoDi.Data
{
    public class CoDiContext : DbContext
    {
        public DbSet<Song> Song { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CoDi_Develop;Username=CoDi;Password=CoDi");
    }
}
