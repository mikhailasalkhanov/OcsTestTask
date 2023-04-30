using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Services;
using Ordering.Domain.Interfaces;

namespace Ordering.Application.Extensions;

public static class ApplicationsServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}