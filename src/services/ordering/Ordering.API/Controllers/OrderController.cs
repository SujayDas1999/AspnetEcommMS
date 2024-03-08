using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.Dtos;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using Ordering.Domain.entities;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByUsername(string username)
        {
            var query = new GetOrdersListQuery(username);
            try
            {
                var orders = await _mediator.Send(query);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CheckoutOrder([FromBody] CreateCheckoutDto checkoutDto)
        {
            var command = new CheckoutOrderCommand(checkoutDto);

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder([FromBody] Order order)
        {
            var command = new UpdateCommand(order);
            var result = await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand()
            {
                Id = id
            };

            var result = await _mediator.Send(command);
            return Ok();
        }
    }
}
