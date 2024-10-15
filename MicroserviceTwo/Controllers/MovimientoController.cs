using MicroserviceTwo.Models;
using MicroserviceTwo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoRepository _repository;
        public MovimientoController(IMovimientoRepository repository)
        {
                _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovimientos()
        {
            var movimientos = await _repository.GetMovimientos();
            return Ok(movimientos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _repository.GetMovimientoById(id);
            if (movimiento == null)
                return NotFound();

            return Ok(movimiento);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovimiento([FromBody] Movimiento movimiento)
        {
            if (movimiento == null)
                return BadRequest();

            await _repository.AddMovimiento(movimiento);
            return CreatedAtAction(nameof(GetMovimientoById), new { id = movimiento.MovimientoId }, movimiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovimiento(int id, [FromBody] Movimiento movimiento)
        {
            if (movimiento == null || id != movimiento.MovimientoId)
                return BadRequest();

            await _repository.UpdateMovimiento(movimiento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            await _repository.DeleteMovimiento(id);
            return NoContent();
        }
    }
}
