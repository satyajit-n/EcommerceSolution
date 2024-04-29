using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CustomerWebAPI.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly IRabbitMQConnection _connection;

        public RabbitMQProducer(IRabbitMQConnection connection)
        {
            _connection = connection;
        }

        public void SendMessage<T>(T message)
        {
            using var channel = _connection.Connection.CreateModel();

            channel.QueueDeclare("customers", exclusive: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "customers", body: body);
        }
    }
}
