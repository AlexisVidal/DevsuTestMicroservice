using MicroserviceOne.Models;

namespace MicroserviceOne.Repositories
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetPersonas();
        Task<Persona> GetPersonaById(int id);
        Task AddPersona(Persona persona);
        Task UpdatePersona(Persona persona);
        Task DeletePersona(int id);
    }
}
