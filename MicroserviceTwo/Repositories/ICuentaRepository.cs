using MicroserviceTwo.Models;

namespace MicroserviceTwo.Repositories
{
    public interface ICuentaRepository
    {
        Task<IEnumerable<Cuenta>> GetCuentas();
        Task<Cuenta> GetCuentaById(int id);
        Task AddCuenta(Cuenta cuenta);
        Task UpdateCuenta(Cuenta cuenta);
        Task DeleteCuenta(int id);
    }
}
