﻿using MicroserviceOne.Dto;
using MicroserviceOne.Models;
using MicroserviceOne.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        private readonly IPersonaRepository _personaRepository;
        public ClienteController(IClienteRepository repository, IPersonaRepository personaRepository)
        {
            _repository = repository;
            _personaRepository = personaRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientes = await _repository.GetClientes();
            var clienteResponseDto = clientes.Select(q => new ClienteResponseDto
            {
                Nombres = q.Persona.Nombre,
                Direccion = q.Persona.Direccion,
                Telefono = q.Persona.Telefono,
                Contrasena = q.Contrasena,
                Estado = q.Estado
            }).ToList();
            return Ok(clienteResponseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            var cliente = await _repository.GetClienteById(id);
            if (cliente == null)
                return NotFound();

            var clienteResponseDto = new ClienteResponseDto
            {
                Nombres = cliente.Persona.Nombre,
                Direccion = cliente.Persona.Direccion,
                Telefono = cliente.Persona.Telefono,
                Contrasena = cliente.Contrasena,
                Estado = cliente.Estado
            };
            return Ok(clienteResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteRequestDto clienteRequestDto)
        {
            if (clienteRequestDto == null)
                return BadRequest();

            Persona persona;
            if (clienteRequestDto.PersonaId != 0)
            {
                return BadRequest();
            }
            else
            {
                persona = new Persona
                {
                    Nombre = clienteRequestDto.Nombre,
                    Genero = clienteRequestDto.Genero,
                    Edad = clienteRequestDto.Edad,
                    Identificacion = clienteRequestDto.Identificacion,
                    Direccion = clienteRequestDto.Direccion,
                    Telefono = clienteRequestDto.Telefono
                };
                try
                {
                    await _personaRepository.AddPersona(persona);
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627) //Unique exceptions
                {
                    return Conflict(new { Message = "Ya existe una persona con esta identificación." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Ocurrió un error al crear la persona.", Details = ex.Message });
                }
            }
            var cliente = new Cliente
            {
                PersonaId = persona.PersonaId,
                Contrasena = clienteRequestDto.Contrasena,
                Estado = true
            };

            try
            {
                await _repository.AddCliente(cliente);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
            {
                return Conflict(new { Message = "Ya existe un cliente con esta identificación." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al crear el cliente.", Details = ex.Message });
            }

            var clienteDto = await GetClienteById(cliente.PersonaId);
            if (clienteDto is IActionResult actionResult)
            {
                return actionResult;
            }
            return StatusCode(500, new { Message = "Ocurrió un error al recuperar el cliente."});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null || id != cliente.PersonaId)
                return BadRequest();

            await _repository.UpdateCliente(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            await _repository.DeleteCliente(id);
            return NoContent();
        }
    }
}
