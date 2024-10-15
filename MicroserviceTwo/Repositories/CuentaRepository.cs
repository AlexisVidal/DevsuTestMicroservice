using MicroserviceTwo.Data;
using MicroserviceTwo.Dto;
using MicroserviceTwo.Models;
using MicroserviceTwo.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTwo.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly AppDbContext _context;
        private readonly RabbitMQConsumer _consumer;

        public CuentaRepository(AppDbContext context, RabbitMQConsumer consumer)
        {
            _context = context;
            _consumer = consumer;
        }

        public async Task<IEnumerable<CuentaClienteDto>> GetCuentas()
        {
            var cuentas = await _context.Cuenta.ToListAsync();

            var cuentaClienteDto = new List<CuentaClienteDto>();

            foreach (var cuenta in cuentas)
            {
                // Aquí envías el PersonaId de la cuenta para obtener los datos de la persona desde MicroserviceOne
                var cliente = await _consumer.ObtenerClienteResponseDtoPorRabbitMQ(cuenta.PersonaId);

                if (cliente == null)
                {
                    cliente = new ClienteResponseDto();
                }

                cuentaClienteDto.Add(new CuentaClienteDto
                {
                    CuentaId = cuenta.CuentaId,
                    NumeroCuenta = cuenta.NumeroCuenta,
                    TipoCuenta = cuenta.TipoCuenta,
                    SaldoInicial = cuenta.SaldoInicial,
                    Estado = cuenta.Estado,
                    Nombres = cliente.Nombres,
                    Direccion = cliente.Direccion,
                    Telefono = cliente.Telefono
                });
            }

            return cuentaClienteDto;
        }


        public async Task<CuentaClienteDto> GetCuentaById(int id)
        {
            var cuenta = await _context.Cuenta.FirstOrDefaultAsync(p => p.CuentaId == id);
            ClienteResponseDto cliente = new ClienteResponseDto();
            var cuentaClienteDto = new CuentaClienteDto
            {
                CuentaId = cuenta.CuentaId,
                NumeroCuenta = cuenta.NumeroCuenta,
                TipoCuenta = cuenta.TipoCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Estado = cuenta.Estado,
                Nombres = cliente.Nombres,
                Direccion = cliente.Direccion,
                Telefono = cliente.Telefono
            };
            return cuentaClienteDto;
        }

        public async Task AddCuenta(Cuenta cuenta)
        {
            await _context.Cuenta.AddAsync(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCuenta(Cuenta cuenta)
        {
            _context.Cuenta.Update(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCuenta(int id)
        {
            var cuenta = await _context.Cuenta.FindAsync(id);
            if (cuenta != null)
            {
                _context.Cuenta.Remove(cuenta);
                await _context.SaveChangesAsync();
            }
        }
    }
}
