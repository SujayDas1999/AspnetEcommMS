using MediatR;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Exceptions;
using Ordering.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {

        private readonly IOrderRepository _orderRepository;
        //private readonly IEmailService _emailService;
        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            //_emailS
        }
        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Order.Id);
            if (order == null)
            {
                Console.WriteLine("--> Order does not exists in database");
                throw new NotFoundException(nameof(Order), request.Order.Id);
            }

            order.AddressLine = request.Order.AddressLine;
            order.CardName = request.Order.CardName;
            order.CardNumber = request.Order.CardNumber;
            order.Expiration = request.Order.Expiration;
            order.Country = request.Order.Country;
            order.CVV = request.Order.CVV;
            order.EmailAddress = request.Order.EmailAddress;
            order.LastModifiedDate = DateTime.Now;
            order.PaymentMethod = request.Order.PaymentMethod;
            order.FirstName = request.Order.FirstName;
            order.LastName = request.Order.LastName;

            await _orderRepository.UpdateAsync(order);

            Console.WriteLine("--> Operation success");

            return Unit.Value;
        }
    }
}
