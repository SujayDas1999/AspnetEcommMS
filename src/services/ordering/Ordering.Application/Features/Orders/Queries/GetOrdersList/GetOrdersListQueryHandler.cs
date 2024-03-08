using MediatR;
using Microsoft.VisualBasic.FileIO;
using Ordering.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    internal class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<List<OrderDto>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderList =  await _orderRepository.GetOrdersByUserName(request.UserName);

            List<OrderDto> orders = new();

            foreach(var item in  orderList)
            {
                orders.Add(new OrderDto
                {
                    Id = item.Id,
                    AddressLine = item.AddressLine,
                    CardName = item.CardName,
                    CardNumber = item.CardNumber,
                    Country = item.Country,
                    CVV = item.CVV,
                    EmailAddress = item.EmailAddress,
                    Expiration = item.Expiration,
                    FirstName  = item.FirstName,
                    LastName = item.LastName,   
                    PaymentMethod = item.PaymentMethod,
                    State = item.State,
                    TotalPrice  = item.TotalPrice,
                    UserName = item.UserName,
                    ZipCode = item.ZipCode,
                });
            }

            return orders;
        }
    }
}
