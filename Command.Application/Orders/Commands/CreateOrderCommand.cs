using MediatR;
using System;

namespace Command.Application.Orders.Commands
{
    // Command برای ساخت سفارش
    public class CreateOrderCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
    }




}
