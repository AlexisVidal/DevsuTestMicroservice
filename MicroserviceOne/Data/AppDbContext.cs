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
            .HasOne(c => c.Persona)
            .WithOne(p => p.Cliente)
            .HasForeignKey<Cliente>(c => c.PersonaId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Cliente>()
            .HasIndex(c => new { c.ClienteId, c.PersonaId })
            .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => new { c.PersonaId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
