using RabbitMQ.Client;
using System.Text;

namespace MicroserviceOne.Services
{
    public class RabbitMQPublisher
    {
        private readonly IModel _channel;

        public RabbitMQPublisher(IModel channel)
        {
            _channel = channel;
        }

        public void Publish(string message)
        {
            _channel.QueueDeclare(queue: "microservicetwo_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "",
                                 routingKey: "microservicetwo_queue",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
