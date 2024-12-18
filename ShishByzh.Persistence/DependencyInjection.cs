using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString)
        {
            Guard.Against.NullOrEmpty(connectionString, message: "Connection string 'DbConnection' not found.");
            
            services.AddDbContext<ShishByzhDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IShishByzhDbContext>(provider => provider.GetService<ShishByzhDbContext>()!);
            return services;
        }
    }
}
