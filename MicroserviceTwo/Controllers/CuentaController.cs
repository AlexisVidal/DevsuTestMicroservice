using MicroserviceTwo.Models;
using MicroserviceTwo.Repositories;
using MicroserviceTwo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaRepository _repository;
        public CuentaController(ICuentaRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCuentas()
        {
            var cuentas = await _repository.GetCuentas();
            return Ok(cuentas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuentaById(int id)
        {
            var cuenta = await _repository.GetCuentaById(id);
            if (cuenta == null)
                return NotFound();

            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCuenta([FromBody] Cuenta cuenta)
        {
            if (cuenta == null)
                return BadRequest();

            await _repository.AddCuenta(cuenta);
            return CreatedAtAction(nameof(GetCuentaById), new { id = cuenta.PersonaId }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuenta(int id, [FromBody] Cuenta cuenta)
        {
            if (cuenta == null)
                return BadRequest();

            await _repository.UpdateCuenta(cuenta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            await _repository.DeleteCuenta(id);
            return NoContent();
        }
    }
}
