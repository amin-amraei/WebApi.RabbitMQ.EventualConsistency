using Command.Domain.Entities;
using Command.Infrastructure.Services;
using Command.Persistence.Context;
using MediatR;
using SharedKernel;

namespace Command.Application.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
    {
        private readonly CommandDbContext _commandDbContext;
        private readonly IEventPublisher _eventPublisher;

        public CreateOrderCommandHandler(CommandDbContext commandDbContext, IEventPublisher eventPublisher)
        {
            _commandDbContext = commandDbContext;
            _eventPublisher = eventPublisher;
        }

        public async Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order { Id = request.Id, ProductName = request.ProductName };
            _commandDbContext.Orders.Add(order);
            await _commandDbContext.SaveChangesAsync(cancellationToken);

            // انتشار Event به RabbitMQ
            var orderCreatedEvent = new OrderCreatedEvent
            {
                Id = order.Id,
                ProductName = order.ProductName
            };

            await _eventPublisher.PublishAsync(orderCreatedEvent);

            return Unit.Value;
        }
    }

}
