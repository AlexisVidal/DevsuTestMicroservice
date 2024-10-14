using MicroserviceTwo.Data;
using MicroserviceTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTwo.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly AppDbContext _context;

        public CuentaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cuenta>> GetCuentas()
        {
            return await _context.Cuenta.Include(p => p.PersonaId).ToListAsync();
        }

        public async Task<Cuenta> GetCuentaById(int id)
        {
            return await _context.Cuenta.Include(p => p.PersonaId).FirstOrDefaultAsync(p => p.CuentaId == id);
        }

        public async Task AddCuenta(Cuenta cuenta)
        {
            await _context.Cuenta.AddAsync(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCuenta(Cuenta cuenta)
        {
            _context.Cuenta.Update(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuenta.FindAsync(id);
            if (cuenta != null)
            {
                _context.Cuenta.Remove(cuenta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
