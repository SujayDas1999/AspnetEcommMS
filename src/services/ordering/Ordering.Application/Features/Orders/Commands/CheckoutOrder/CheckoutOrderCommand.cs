using MediatR;
using Ordering.Application.Features.Orders.Commands.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<Guid>
    {
        public CreateCheckoutDto CreateCheckoutDto { get; set; }

        public CheckoutOrderCommand(CreateCheckoutDto dto)
        {
            CreateCheckoutDto = dto;
        }
    }
}
