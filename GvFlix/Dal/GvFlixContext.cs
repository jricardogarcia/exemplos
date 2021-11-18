using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class GvFlixContext : DbContext
    {
        public GvFlixContext(DbContextOptions<GvFlixContext> options) : base(options)
        {
        }

        public DbSet<Ator> Ator { get; set; }
        public DbSet<Diretor> Diretor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("dbo");

            modelBuilder
                .Entity<Ator>();

            modelBuilder
                .Entity<Diretor>();
        }
    }
}
