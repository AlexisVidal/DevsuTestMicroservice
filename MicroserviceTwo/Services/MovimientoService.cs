using MicroserviceTwo.Models;
using MicroserviceTwo.Repositories;

namespace MicroserviceTwo.Services
{
    public class MovimientoService : IMovimientoService
    {
        private readonly IMovimientoRepository _repository;
        public MovimientoService(IMovimientoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Movimiento>> GetMovimientos()
        {
            return await _repository.GetMovimientos();
        }

        public async Task<Movimiento> GetMovimientoById(int id)
        {
            return await _repository.GetMovimientoById(id);
        }

        public async Task AddMovimiento(Movimiento movimiento)
        {
            await _repository.AddMovimiento(movimiento);
        }

        public async Task UpdateMovimiento(Movimiento movimiento)
        {
            await _repository.UpdateMovimiento(movimiento);
        }

        public async Task DeleteMovimiento(int id)
        {
            await _repository.DeleteMovimiento(id);
        }
    }
}
