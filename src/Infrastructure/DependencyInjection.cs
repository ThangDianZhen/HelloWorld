using Ignite.Application.Interfaces.Persistence;
using Ignite.Application.Interfaces.Persistence.Repositories;
using Ignite.Infrastructure.Persistence;
using Ignite.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ignite.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default")
            ?? "Data Source=ignite.db";

        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<ICampusRepository, CampusRepository>();
        services.AddScoped<ISermonRepository, SermonRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IPageRepository, PageRepository>();
        services.AddScoped<IMinistryRepository, MinistryRepository>();
        services.AddScoped<IConnectRequestRepository, ConnectRequestRepository>();
        services.AddScoped<IChurchSettingsRepository, ChurchSettingsRepository>();

        return services;
    }
}
