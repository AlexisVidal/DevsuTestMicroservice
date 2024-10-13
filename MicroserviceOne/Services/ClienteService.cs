using MicroserviceOne.Models;
using MicroserviceOne.Repositories;

namespace MicroserviceOne.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            return await _clienteRepository.GetClientes();
        }
        public async Task<Cliente> GetClienteById(int id)
        {
            return await _clienteRepository.GetClienteById(id);
        }
        public async Task AddCliente(Cliente persona)
        {
            await _clienteRepository.AddCliente(persona);
        }
        public Task UpdateCliente(Cliente persona)
        {
            throw new NotImplementedException();
        }
        public Task DeleteCliente(int id)
        {
            throw new NotImplementedException();
        }


        

    }
}
