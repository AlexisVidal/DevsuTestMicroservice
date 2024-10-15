using MicroserviceOne.Data;
using MicroserviceOne.Dto;
using MicroserviceOne.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceOne.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _context.Cliente
                .Include(q => q.Persona)
                .ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Cliente
                .Include(q => q.Persona)
                .FirstOrDefaultAsync(p => p.ClienteId == id);
        }

        public async Task AddCliente(Cliente cliente)
        {
            await _context.Cliente.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            _context.Cliente.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                cliente.Estado = false;
                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
