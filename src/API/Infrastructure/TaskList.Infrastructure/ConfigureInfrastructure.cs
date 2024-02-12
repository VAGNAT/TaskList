using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Application.Interfaces;
using TaskList.Infrastructure.Persistance;

namespace TaskList.Infrastructure
{
    /// <summary>
    /// Static class for configuring and initializing infrastructure services.
    /// </summary>
    public static class ConfigureInfrastructure
    {
        /// <summary>
        /// Adds infrastructure services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The configuration for the application.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var conStrBuilder = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("TaskListContext"))
            {
                Password = configuration["PostgresPassword"],
                Username = configuration["PostgresUsername"],
            };

            var plumsailContext = conStrBuilder.ConnectionString;
            services.AddDbContext<TaskListContext>(options => options.UseNpgsql(plumsailContext,
                x => x.MigrationsAssembly("TaskList.Infrastructure.PostgreSql")));

            services.AddTransient<IApplicationContext>(provider => provider.GetRequiredService<TaskListContext>());

            return services;
        }

        /// <summary>
        /// Initializes infrastructure services using the specified <see cref="IServiceProvider"/>.
        /// </summary>
        /// <param name="provider">The <see cref="IServiceProvider"/> used to retrieve services.</param>
        public static void InitializeInfrastructureServices(this IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TaskListContext>();

            dbContext.Database.EnsureCreated();
        }
    }
}
