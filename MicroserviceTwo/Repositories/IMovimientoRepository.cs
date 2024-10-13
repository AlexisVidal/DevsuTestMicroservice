using MicroserviceTwo.Models;

namespace MicroserviceTwo.Repositories
{
    public interface IMovimientoRepository
    {
        Task<IEnumerable<Movimiento>> GetMovimientos();
        Task<Movimiento> GetMovimientoById(int id);
        Task AddMovimiento(Movimiento movimiento);
        Task UpdateMovimiento(Movimiento movimiento);
        Task DeleteMovimiento(int id);
    }
}
