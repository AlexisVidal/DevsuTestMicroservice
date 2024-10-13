using MicroserviceTwo.Data;
using MicroserviceTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTwo.Repositories
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly AppDbContext _context;

        public MovimientoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movimiento>> GetMovimientos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        public async Task<Movimiento> GetMovimientoById(int id)
        {
            return await _context.Movimientos.FirstOrDefaultAsync(p => p.MovimientoId == id);
        }

        public async Task AddMovimiento(Movimiento movimiento)
        {
            await _context.Movimientos.AddAsync(movimiento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovimiento(Movimiento movimiento)
        {
            _context.Movimientos.Update(movimiento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento != null)
            {
                _context.Movimientos.Remove(movimiento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
