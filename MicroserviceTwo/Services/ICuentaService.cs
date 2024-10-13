using MicroserviceTwo.Models;

namespace MicroserviceTwo.Services
{
    public interface ICuentaService
    {
        Task<IEnumerable<Cuenta>> GetCuentas();
        Task<Cuenta> GetCuentaById(int id);
        Task AddCuenta(Cuenta cuenta);
        Task UpdateCuenta(Cuenta cuenta);
        Task DeleteCuenta(int id);
    }
}
