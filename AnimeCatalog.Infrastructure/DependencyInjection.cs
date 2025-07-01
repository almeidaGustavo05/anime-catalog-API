using AnimeCatalog.Domain.Interfaces;
using AnimeCatalog.Infrastructure.Data;
using AnimeCatalog.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimeCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AnimeCatalogDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IAnimeRepository, AnimeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}