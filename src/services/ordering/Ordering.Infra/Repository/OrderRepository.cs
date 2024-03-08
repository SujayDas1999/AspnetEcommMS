using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.entities;
using Ordering.Infra.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infra.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            //_context.Set<Order>().Where(o => o.UserName == userName).ToList(); 

            return await _context.Orders.Where(o => o.UserName == userName).ToListAsync();
        }
    }
}
