using Bibi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlueModas.Repository.Context
{
    public class BibiContext : DbContext
    {
        public BibiContext(DbContextOptions<BibiContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Arquivo> Arquivo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BibiContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}