using MicroserviceOne.Events;
using MicroserviceOne.Models;
using MicroserviceOne.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MicroserviceOne.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepository _repository;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        public PersonaService(IPersonaRepository repository,RabbitMQPublisher rabbitMQPublisher)
        {
            _repository = repository;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<IEnumerable<Persona>> GetPersonas()
        {
            return await _repository.GetPersonas();
        }

        public async Task<Persona> GetPersonaById(int id)
        {
            return await _repository.GetPersonaById(id);
        }

        public async Task AddPersona(Persona persona)
        {
            await _repository.AddPersona(persona);
        }

        public async Task UpdatePersona(Persona persona)
        {
            await _repository.UpdatePersona(persona);
        }

        public async Task DeletePersona(int id)
        {
            await _repository.DeletePersona(id);
        }
    }
}
