using Ordering.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infra.Persistance
{
    public class OrderSeed
    {
        public static async Task SeedAsync(OrderContext orderContext)
        {
            if(!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(PreConfiguredOrders());
                await orderContext.SaveChangesAsync();
                Console.WriteLine("--> Save Success in order db!");
            }
        }

        private static List<Order> PreConfiguredOrders()
        {
            return new List<Order>
            {
                new()
                {
                    UserName = "swn", FirstName = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350
                }
            };
        }
    }
}
