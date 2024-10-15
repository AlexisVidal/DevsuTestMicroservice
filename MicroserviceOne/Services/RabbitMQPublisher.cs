using MicroserviceOne.Dto;
using Newtonsoft.Json;
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
        public void PublishResponse(ClienteResponseDto clienteDto, string replyToQueue, string correlationId)
        {
            var message = JsonConvert.SerializeObject(clienteDto);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.CorrelationId = correlationId; // Asegúrate de que el CorrelationId se establezca correctamente

            _channel.BasicPublish(exchange: "",
                                 routingKey: replyToQueue,
                                 basicProperties: properties,
                                 body: body);
        }
    }
}
