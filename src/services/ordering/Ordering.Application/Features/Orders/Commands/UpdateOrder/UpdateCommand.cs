using MediatR;
using Ordering.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateCommand: IRequest<Unit>
    {
        public Order Order { get; set; }

        public UpdateCommand(Order order)
        {
            Order = order;
        }

    }
}
