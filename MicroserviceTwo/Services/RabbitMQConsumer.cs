using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace MicroserviceTwo.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IModel _channel;

        public RabbitMQConsumer(IModel channel)
        {
            _channel = channel;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _channel.QueueDeclare(queue: "microserviciotwo_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Mensaje recibido: {message}");
            };

            _channel.BasicConsume(queue: "microserviciotwo_queue",
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}