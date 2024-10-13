using MicroserviceOne.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceOne.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
