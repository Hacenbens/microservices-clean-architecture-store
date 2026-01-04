using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Persistence;
using OrderService.Application.Abstractions.Repositories;
using OrderService.Infrastructure.Persistence.Repositories;


namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("OrderDatabase")));
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}