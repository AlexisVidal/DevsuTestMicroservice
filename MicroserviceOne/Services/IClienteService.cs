using MicroserviceOne.Models;

namespace MicroserviceOne.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetClientes();
        Task<Cliente> GetClienteById(int id);
        Task AddCliente(Cliente persona);
        Task UpdateCliente(Cliente persona);
        Task DeleteCliente(int id);
    }
}
