using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extentions
{
    public static class HostExtensions
    {
        public static WebApplication MigrateDatabase<TContext>(this WebApplication app, Action<TContext, IServiceProvider> seeder, int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<TContext>();

            try
            {
                Console.WriteLine("--> Migration Started!");
                InvokeSeeder(seeder, context, scope.ServiceProvider);
                Console.WriteLine("--> Migration Completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Seeding failed due to {ex.Message}");
                if(retryForAvailability < 50)
                {
                    retryForAvailability++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(app, seeder, retryForAvailability);
                }
                //throw;
            }

            return app;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider service) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, service);
        }
    }
}
