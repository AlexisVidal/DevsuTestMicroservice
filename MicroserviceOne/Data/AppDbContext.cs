using MicroserviceOne.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceOne.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente")
                .HasBaseType<Persona>(); // Cliente hereda de Persona

            base.OnModelCreating(modelBuilder);
        }
    }
}
