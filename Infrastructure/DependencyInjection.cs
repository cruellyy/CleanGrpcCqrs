using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
#if RELEASE
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?.Replace("{NEVA_SMS_DB_PASSWORD}", Environment.GetEnvironmentVariable("NEVA_SMS_DB_PASSWORD") ?? "");
#elif DEBUG
        var connectionString = configuration.GetConnectionString("DefaultConnection");
#endif
        
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    }
}