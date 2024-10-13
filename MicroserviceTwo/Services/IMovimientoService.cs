using MicroserviceTwo.Models;

namespace MicroserviceTwo.Services
{
    public interface IMovimientoService
    {
        Task<IEnumerable<Movimiento>> GetMovimientos();
        Task<Movimiento> GetMovimientoById(int id);
        Task AddMovimiento(Movimiento movimiento);
        Task UpdateMovimiento(Movimiento movimiento);
        Task DeleteMovimiento(int id);
    }
}
