using Command.Application.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAppRabbitMQ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;        

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;            
        }
       

        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            return Ok(await _mediator.Send(command));
        }    
    }
}
