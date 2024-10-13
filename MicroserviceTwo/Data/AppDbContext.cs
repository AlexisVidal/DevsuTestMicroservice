using MicroserviceTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTwo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
    }
}
