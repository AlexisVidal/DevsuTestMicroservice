using MicroserviceOne.Models;
using MicroserviceOne.Repositories;
using MicroserviceOne.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _repository;
        public PersonaController(IPersonaRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonas()
        {
            var personas = await _repository.GetPersonas();
            return Ok(personas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonaById(int id)
        {
            var persona = await _repository.GetPersonaById(id);
            if (persona == null)
                return NotFound();

            return Ok(persona);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] Persona persona)
        {
            if (persona == null)
                return BadRequest();

            await _repository.AddPersona(persona);
            return CreatedAtAction(nameof(GetPersonaById), new { id = persona.PersonaId }, persona);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, [FromBody] Persona persona)
        {
            if (persona == null || id != persona.PersonaId)
                return BadRequest();

            await _repository.UpdatePersona(persona);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            await _repository.DeletePersona(id);
            return NoContent();
        }
    }
}
