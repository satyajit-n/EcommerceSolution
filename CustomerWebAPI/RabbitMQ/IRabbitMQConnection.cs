using RabbitMQ.Client;

namespace CustomerWebAPI.RabbitMQ
{
    public interface IRabbitMQConnection
    {
        IConnection Connection {  get; } 
    }
}
