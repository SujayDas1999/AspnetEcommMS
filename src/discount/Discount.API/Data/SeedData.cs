using Discount.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data
{
    public class SeedData
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            SeedDataInDb(scope.ServiceProvider.GetRequiredService<DiscountContext>());
        }

        public static void SeedDataInDb(DiscountContext context)
        {
            context.Database.Migrate();

            if(!context.Coupons.Any()) 
            {
                Console.WriteLine("--> No Data Found! Seeding in process!");

                List<Coupon> coupons = new List<Coupon>
                {
                    new Coupon
                    {
                        Amount = 150,
                        Description = "IPhone Discount",
                        ProductName = "IPhone X"
                    },

                    new Coupon
                    {
                        Amount = 100,
                        Description = "Samsung Discount",
                        ProductName = "Samsung 10"
                    }
                };

                context.Coupons.AddRange(coupons);
                context.SaveChanges();

             

                Console.WriteLine("--> Seeding Completed");
                return;
            }

            Console.WriteLine("--> Data Found!");
        }
    }
}
