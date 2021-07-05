using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Context;
using WebApplication1.Service;

namespace WebApplication1
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuretion)
        {
            var connectionString = configuretion["DbConnection"];
            services.AddDbContext<abTestdbContext>(option => option.UseNpgsql(configuretion.GetConnectionString("DbConnection")));

            services.AddTransient<IUsersManagerService, UsersManagerService>();
            return services;
        }
    }
}
