using ChTi.Client.Abstraction;
using ChTi.Client.Cache;
using ChTi.Client.Implemention;
using Microsoft.Extensions.DependencyInjection;

namespace ChTi.Client.StartUp;

public static class StartUp
{
    public static IServiceCollection AddFteamChTiService(this IServiceCollection service)
    {
        service.AddTransient<ICache, Cache.Cache>();
        service.AddTransient<IGrpcRule, GrpcService>();

        return service;
    }
}
