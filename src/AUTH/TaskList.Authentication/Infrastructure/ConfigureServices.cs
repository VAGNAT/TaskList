using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace TaskList.Authentication.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var portString = configuration["PostgresPort"];
            portString = string.IsNullOrEmpty(portString) ? "5432" : portString;
            int port = int.Parse(portString);

            var conStrBuilder = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("AuthDbContext"))
            {
                Password = configuration["PostgresPassword"],
                Host = configuration["PostgresHost"],
                Port = port,
                Username = configuration["PostgresUsername"],
                Database = configuration["PostgresDatabase"]
            };
            var applicationContext = conStrBuilder.ConnectionString;
            services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(applicationContext
                , x => x.MigrationsAssembly("TaskList.Authentication")));

            return services;
        }

        public static async void InitializeInfrastructureServices(this IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
