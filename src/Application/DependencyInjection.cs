using Ignite.Application.Interfaces.Services.Application;
using Ignite.Application.Interfaces.Services.Entities;
using Ignite.Application.Services.Application;
using Ignite.Application.Services.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Ignite.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICampusService, CampusService>();
        services.AddScoped<ISermonService, SermonService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IPageService, PageService>();
        services.AddScoped<IMinistryService, MinistryService>();
        services.AddScoped<IConnectRequestService, ConnectRequestService>();
        services.AddScoped<IChurchSettingsService, ChurchSettingsService>();
        services.AddScoped<IChurchSiteService, ChurchSiteService>();
        return services;
    }
}
