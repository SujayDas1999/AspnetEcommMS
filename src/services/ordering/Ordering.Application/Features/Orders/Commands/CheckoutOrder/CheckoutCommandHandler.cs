using MediatR;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;
        public CheckoutCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            //_emailService = emailService;
        }

        public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                AddressLine = request.CreateCheckoutDto.AddressLine,
                CardName = request.CreateCheckoutDto.CardName,
                CardNumber = request.CreateCheckoutDto.CardNumber,
                Country = request.CreateCheckoutDto.Country,
                CVV =   request.CreateCheckoutDto.CVV,
                EmailAddress = request.CreateCheckoutDto.EmailAddress,
                Expiration = request.CreateCheckoutDto.Expiration,
                FirstName = request.CreateCheckoutDto.FirstName,
                LastName = request.CreateCheckoutDto.LastName,
                PaymentMethod = request.CreateCheckoutDto.PaymentMethod,
                State = request.CreateCheckoutDto.State,
                TotalPrice = request.CreateCheckoutDto.TotalPrice,
                UserName = request.CreateCheckoutDto.UserName,
                ZipCode = request.CreateCheckoutDto.ZipCode,
            };

            var newOrder = await _orderRepository.AddAsync(order);

            Console.WriteLine("--> Order is successfully created");

            return newOrder.Id;
        }
    }
}
