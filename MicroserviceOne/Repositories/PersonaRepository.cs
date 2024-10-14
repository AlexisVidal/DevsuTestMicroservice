using MicroserviceOne.Data;
using MicroserviceOne.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceOne.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly AppDbContext _context;

        public PersonaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetPersonas()
        {
            return await _context.Persona.ToListAsync();
        }

        public async Task<Persona> GetPersonaById(int id)
        {
            return await _context.Persona.FirstOrDefaultAsync(p => p.PersonaId == id);
        }

        public async Task AddPersona(Persona persona)
        {
            await _context.Persona.AddAsync(persona);
            await _context.SaveChangesAsync();
            _context.Entry(persona).State = EntityState.Detached;
        }

        public async Task UpdatePersona(Persona persona)
        {
            _context.Persona.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersona(int id)
        {
            var persona = await _context.Persona.FindAsync(id);
            if (persona != null)
            {
                _context.Persona.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }
    }
}