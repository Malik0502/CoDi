using CoDi.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoDi.Data
{
    public class CoDiContext : DbContext
    {
        public DbSet<Song> Song { get; set; }

        public DbSet<DailySongStats> DailySongStats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CoDi_Develop;Username=CoDi;Password=CoDi");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>(x =>
            {
                x.ToTable("Song");
                x.HasKey(x => x.Id);
                x.HasMany(x => x.DailyStats)
                    .WithOne(x => x.Song)
                    .HasForeignKey(x => x.SongId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DailySongStats>(x =>
            {
                x.ToTable("daily_song_stats");
                x.HasKey(x => x.Id);
            });
        }
    }
}
