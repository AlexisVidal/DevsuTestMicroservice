using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using MicroserviceTwo.Dto;

namespace MicroserviceTwo.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public RabbitMQConsumer()
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            DeclareQueues();
        }
        private void DeclareQueues()
        {
            try
            {
                _channel.QueueDeclare(queue: "clientes_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                _channel.QueueDeclare(queue: "microservicetwo_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                Console.WriteLine("Colas declaradas correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al declarar colas: {ex.Message}");
            }
        }
        public async Task<ClienteResponseDto> ObtenerClienteResponseDtoPorRabbitMQ(int clienteId)
        {
            var tcs = new TaskCompletionSource<ClienteResponseDto>();

            // Configurar y publicar la solicitud
            var requestQueue = "clientes_queue";
            var responseQueue = "microservicetwo_queue";  // Cola de respuesta

            // Declarar la cola de respuesta (asegúrate de que exista)
            _channel.QueueDeclare(queue: responseQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            // Crear el mensaje de solicitud con un identificador único (correlationId)
            var correlationId = Guid.NewGuid().ToString();
            var message = JsonConvert.SerializeObject(new { ClienteId = clienteId });
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = correlationId;
            properties.ReplyTo = "microservicetwo_queue";

            _channel.BasicPublish(exchange: "", routingKey: "clientes_queue", basicProperties: properties, body: body);

            // Esperar la respuesta
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    var responseMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var clienteResponseDto = JsonConvert.DeserializeObject<ClienteResponseDto>(responseMessage);
                    tcs.SetResult(clienteResponseDto);

                    Console.WriteLine($"Respuesta recibida para PersonaId: {clienteId}");
                }
                else
                {
                    Console.WriteLine("CorrelationId no coincide. Ignorando mensaje.");
                }
            };



            // Iniciar la escucha en la cola de respuestas
            _channel.BasicConsume(queue: responseQueue, autoAck: true, consumer: consumer);

            // Añadir un timeout para evitar que la tarea quede colgada indefinidamente
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
            var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

            if (completedTask == timeoutTask)
            {
                // Si el tiempo expira, lanzar una excepción o devolver un valor predeterminado
                //throw new TimeoutException("Timeout esperando respuesta de RabbitMQ.");
                return null;
            }

            return await tcs.Task;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Aquí puedes declarar la cola que deseas escuchar
            _channel.QueueDeclare(queue: "clientes_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var responseMessage = Encoding.UTF8.GetString(ea.Body.ToArray());
                var clienteResponseDto = JsonConvert.DeserializeObject<ClienteResponseDto>(responseMessage);

                Console.WriteLine($"Cliente recibido: {clienteResponseDto.Nombres}");
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