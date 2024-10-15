using MicroserviceOne.Dto;
using MicroserviceOne.Repositories;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MicroserviceOne.Services
{
    public class RabbitMQResponder : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly ClienteRepository _repository;
        private readonly RabbitMQPublisher _publisher;

        public RabbitMQResponder(IModel channel, ClienteRepository repository, RabbitMQPublisher publisher)
        {
            try
            {
                _repository = repository;
                _publisher = publisher;
                _channel = channel;

                // Declarar la cola si es necesario
                _channel.QueueDeclare(queue: "clientes_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en RabbitMQResponder: {ex.Message}");
                throw; // Re-lanzar la excepción después de registrarla
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var request = JsonConvert.DeserializeObject<ClienteResponseDto>(message);

                // Obtener los datos del cliente desde el repositorio
                var cliente = await _repository.GetClienteById(request.ClienteId);

                if (cliente != null)
                {
                    var clienteResponseDto = new ClienteResponseDto
                    {
                        Nombres = cliente.Persona.Nombre,
                        Direccion = cliente.Persona.Direccion,
                        Telefono = cliente.Persona.Telefono,
                        Contrasena = cliente.Contrasena,
                        Estado = cliente.Estado
                    };
                    _publisher.PublishResponse(clienteResponseDto, ea.BasicProperties.ReplyTo, ea.BasicProperties.CorrelationId);
                    Console.WriteLine($"Respuesta enviada para ClienteId: {request.ClienteId}");
                }
                else
                {
                    Console.WriteLine("No se encontró el cliente.");
                }
            };

            _channel.BasicConsume(queue: "clientes_queue", autoAck: true, consumer: consumer);
            return Task.CompletedTask;

        }
        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}