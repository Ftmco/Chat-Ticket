using ChTi.Service.Implemention.Base;
using Identity.Client.StartUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ChTi.Service.Implemention.Injector;

public static class Injector
{
    public static async Task<IServiceCollection> AddChTiServicesAsync(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>((service) =>
        {
            string? connectionString = configuration.GetConnectionString("ChTi_Db");
            return new MongoClient(connectionString);
        });
        await services.AddBaseCudAsync();
        await services.AddBaseQueryAsync();
        await services.AddServicesAsync();
        await services.AddToolsAsync();
        return services;
    }

    public static Task<IServiceCollection> AddBaseQueryAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseQuery<Ticket>, BaseQuery<Ticket>>();
        services.AddScoped<IBaseQuery<Attachment>, BaseQuery<Attachment>>();
        services.AddScoped<IBaseQuery<Chat>, BaseQuery<Chat>>();
        services.AddScoped<IBaseQuery<ChatsUsers>, BaseQuery<ChatsUsers>>();
        services.AddScoped<IBaseQuery<Message>, BaseQuery<Message>>();
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseCud<Ticket>, BaseCud<Ticket>>();
        services.AddScoped<IBaseCud<Attachment>, BaseCud<Attachment>>();
        services.AddScoped<IBaseCud<Chat>, BaseCud<Chat>>();
        services.AddScoped<IBaseCud<ChatsUsers>, BaseCud<ChatsUsers>>();
        services.AddScoped<IBaseCud<Message>, BaseCud<Message>>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
    {
        services.AddTransient<ITicketGet, TicketGet>();
        services.AddTransient<ITicketAction, TicketAction>();
        services.AddTransient<ITicketViewModel, TicketViewModel>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services)
    {
        services.AddFteamIdentityService();
        return Task.FromResult(services);
    }
}