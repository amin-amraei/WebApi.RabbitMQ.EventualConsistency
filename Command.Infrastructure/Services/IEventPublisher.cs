using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SharedKernel;
using System.Text;
using System.Text.Json;

namespace Command.Infrastructure.Services
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event);
    }

    public class RabbitMqEventPublisher : IEventPublisher, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqEventPublisher(IOptions<RabbitMQSettings> options)
        {
            var settings = options.Value;
            var factory = new ConnectionFactory() { HostName = settings.HostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task PublishAsync<T>(T @event)
        {
            var eventName = typeof(T).Name;
            _channel.QueueDeclare(queue: eventName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "",
                                  routingKey: eventName,
                                  basicProperties: properties,
                                  body: body);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }

}
