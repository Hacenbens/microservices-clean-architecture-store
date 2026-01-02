using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Infrastructure.Persistence;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Infrastructure.Persistence.Repositories;


namespace ProductService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProductDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("ProductDatabase")));
		services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}