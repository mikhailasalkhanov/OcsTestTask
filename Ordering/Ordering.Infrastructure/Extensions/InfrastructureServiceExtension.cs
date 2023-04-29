using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain;
using Ordering.Infrastructure.Interfaces;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure.Extensions;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("OrderingConnectionString")));
        services.AddScoped<IRepository<Order>, OrderRepository>();

        return services;
    }
}