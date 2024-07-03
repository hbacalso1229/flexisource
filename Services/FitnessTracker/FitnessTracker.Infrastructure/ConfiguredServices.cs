using Fitness.Infrastructure.Persistence.Repositories;
using FitnessTracker.Application.Common.Interfaces;
using FitnessTracker.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessTracker.Infrastructure
{
    public static class ConfiguredServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO
            string connectionString = configuration["ConnectionString:Mysql"].ToString();

            services.AddScoped<FitnessTrackerDbContext>(provider => provider.GetRequiredService<FitnessTrackerDbContext>());
            services.AddDbContext<FitnessTrackerDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), opt =>
                {
                    opt.MigrationsAssembly(typeof(FitnessTrackerDbContext).Assembly.FullName);
                    opt.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            },
                ServiceLifetime.Scoped
            );

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<FitnessTrackerDbContext>());           
            services.AddScoped<IFitnessTrackerRepository, FitnessTrackerRepository>();

            return services;
        }
    }
}
