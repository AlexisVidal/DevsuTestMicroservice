using MicroserviceOne.Dto;
using MicroserviceOne.Models;

namespace MicroserviceOne.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetClienteById(int id);
        Task AddCliente(Cliente cliente);
        Task UpdateCliente(Cliente cliente);
        Task DeleteCliente(int id);
    }
}
