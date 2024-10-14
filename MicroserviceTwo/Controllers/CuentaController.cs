using MicroserviceTwo.Models;
using MicroserviceTwo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCuentas()
        {
            var cuentas = await _cuentaService.GetCuentas();
            return Ok(cuentas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuentaById(int id)
        {
            var cuenta = await _cuentaService.GetCuentaById(id);
            if (cuenta == null)
                return NotFound();

            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCuenta([FromBody] Cuenta cuenta)
        {
            if (cuenta == null)
                return BadRequest();

            await _cuentaService.AddCuenta(cuenta);
            return CreatedAtAction(nameof(GetCuentaById), new { id = cuenta.PersonaId }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCuenta(int id, [FromBody] Cuenta cuenta)
        {
            if (cuenta == null || id != cuenta.PersonaId)
                return BadRequest();

            await _cuentaService.UpdateCuenta(cuenta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta(int id)
        {
            await _cuentaService.DeleteCuenta(id);
            return NoContent();
        }
    }
}
