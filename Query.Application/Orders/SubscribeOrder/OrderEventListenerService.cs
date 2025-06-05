using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Query.Domain.Entities;
using Query.Persistence.Context;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharedKernel;
using System.Text;
using System.Text.Json;

namespace Query.Application.Orders.SubscribeOrder
{
    public class OrderEventListenerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private IConnection _connection;
        private IModel _channel;

        public OrderEventListenerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "OrderCreatedEvent",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += async (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

                    using var scope = _serviceProvider.CreateScope();
                    var queryDbContext = scope.ServiceProvider.GetRequiredService<QueryDbContext>();

                    // Idempotency: اگر رکورد وجود داشت نگذار دوباره اضافه شود
                    var existing = await queryDbContext.Orders.FindAsync(orderCreatedEvent.Id);
                    if (existing == null)
                    {
                        var orderReadModel = new OrderReadModel
                        {
                            Id = orderCreatedEvent.Id,
                            ProductName = orderCreatedEvent.ProductName,
                            CreatedAt = DateTime.UtcNow
                        };

                        queryDbContext.Orders.Add(orderReadModel);
                        await queryDbContext.SaveChangesAsync();
                    }

                    _channel.BasicAck(ea.DeliveryTag, multiple: false);
                };

                _channel.BasicConsume(queue: "OrderCreatedEvent",
                                      autoAck: false,
                                      consumer: consumer);


                await Task.Delay(1000, stoppingToken);
                //return Task.CompletedTask;
            }
            //return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }

}
