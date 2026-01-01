using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Abstractions.Irepositories;
using UserService.Infrastructure.Persistance;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("UserDb")
                )
            );
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}