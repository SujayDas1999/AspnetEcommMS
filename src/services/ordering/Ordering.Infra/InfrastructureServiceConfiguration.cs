using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.entities;
using Ordering.Infra.Persistance;
using Ordering.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infra
{
    public static class InfrastructureServiceConfiguration
    {
        public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("OrderingConnectionString"));
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
