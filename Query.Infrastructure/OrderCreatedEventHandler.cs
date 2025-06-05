namespace WebAppRabbitMQ
{
    //public class OrderCreatedEventHandler
    //{
    //    private readonly QueryDbContext _context;
    //    private readonly IConnection _connection;

    //    public OrderCreatedEventHandler(QueryDbContext context)
    //    {
    //        _context = context;

    //        var factory = new ConnectionFactory() { HostName = "localhost" };
    //        _connection = factory.CreateConnection();
    //    }

    //    public void StartListening()
    //    {
    //        var channel = _connection.CreateModel();
    //        channel.QueueDeclare(queue: "order_events", durable: false, exclusive: false, autoDelete: false);

    //        var consumer = new EventingBasicConsumer(channel);
    //        consumer.Received += async (model, ea) =>
    //        {
    //            var body = ea.Body.ToArray();
    //            var message = Encoding.UTF8.GetString(body);
    //            var @event = JsonSerializer.Deserialize<OrderCreatedEvent>(message);

    //            var orderReadModel = new OrderReadModel
    //            {
    //                Id = @event.Id,
    //                ProductName = @event.ProductName,
    //                CreatedAt = DateTime.UtcNow
    //            };

    //            _context.Orders.Add(orderReadModel);
    //            await _context.SaveChangesAsync();
    //        };

    //        channel.BasicConsume(queue: "order_events", autoAck: true, consumer: consumer);
    //    }
    //}   
}
