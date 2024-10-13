using MicroserviceTwo.Models;
using MicroserviceTwo.Repositories;

namespace MicroserviceTwo.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ICuentaRepository _repository;
        public CuentaService(ICuentaRepository repository)
        {
                _repository = repository;
        }
        public async Task<IEnumerable<Cuenta>> GetCuentas()
        {
            return await _repository.GetCuentas();
        }

        public async Task<Cuenta> GetCuentaById(int id)
        {
            return await _repository.GetCuentaById(id);
        }

        public async Task AddCuenta(Cuenta cuenta)
        {
            await _repository.AddCuenta(cuenta);
        }

        public async Task UpdateCuenta(Cuenta cuenta)
        {
            await _repository.UpdateCuenta(cuenta);
        }

        public async Task DeleteCuenta(int id)
        {
            await _repository.DeleteCuenta(id);
        }
    }
}
