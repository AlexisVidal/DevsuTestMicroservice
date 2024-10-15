using MicroserviceTwo.Dto;
using MicroserviceTwo.Models;

namespace MicroserviceTwo.Repositories
{
    public interface ICuentaRepository
    {
        //Task<IEnumerable<Cuenta>> GetCuentas();
        Task<CuentaClienteDto> GetCuentaById(int id);
        Task AddCuenta(Cuenta cuenta);
        Task UpdateCuenta(Cuenta cuenta);
        Task DeleteCuenta(int id);
        Task<IEnumerable<CuentaClienteDto>> GetCuentas();
    }
}
