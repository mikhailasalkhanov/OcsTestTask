using Microsoft.Extensions.DependencyInjection;
using Ordering.Abstraction.Services;
using Ordering.Application.Services;

namespace Ordering.Application.Extensions;

public static class ApplicationsServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}