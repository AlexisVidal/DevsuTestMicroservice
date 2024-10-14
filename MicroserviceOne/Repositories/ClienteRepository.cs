using MicroserviceOne.Data;
using MicroserviceOne.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceOne.Repositories
{
    public class ClienteRepository:IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _context.Cliente.Select(c => new Cliente
            {
                PersonaId = c.PersonaId,
                Nombre = c.Nombre,
                Genero = c.Genero,
                Edad = c.Edad,
                Direccion = c.Direccion,
                Identificacion = c.Identificacion,
                Telefono = c.Telefono,
                Contrasena = c.Contrasena,
                Estado = c.Estado
            })
        .ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Cliente
                .Select(c => new Cliente
            {
                PersonaId = c.PersonaId,
                Nombre = c.Nombre,
                Genero = c.Genero,
                Edad = c.Edad,
                Direccion = c.Direccion,
                Identificacion = c.Identificacion,
                Telefono = c.Telefono,
                Contrasena = c.Contrasena,
                Estado = c.Estado
            }).FirstOrDefaultAsync(p => p.PersonaId == id);
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
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
