using MicroserviceTwo.Models;
using MicroserviceTwo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoService _movimientoService;
        [HttpGet]
        public async Task<IActionResult> GetMovimientos()
        {
            var movimientos = await _movimientoService.GetMovimientos();
            return Ok(movimientos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovimientoById(int id)
        {
            var movimiento = await _movimientoService.GetMovimientoById(id);
            if (movimiento == null)
                return NotFound();

            return Ok(movimiento);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovimiento([FromBody] Movimiento movimiento)
        {
            if (movimiento == null)
                return BadRequest();

            await _movimientoService.AddMovimiento(movimiento);
            return CreatedAtAction(nameof(GetMovimientoById), new { id = movimiento.MovimientoId }, movimiento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovimiento(int id, [FromBody] Movimiento movimiento)
        {
            if (movimiento == null || id != movimiento.MovimientoId)
                return BadRequest();

            await _movimientoService.UpdateMovimiento(movimiento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            await _movimientoService.DeleteMovimiento(id);
            return NoContent();
        }
    }
}
