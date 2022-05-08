﻿using ChTi.DataBase.Entity;
using ChTi.Service.Abstraction.Base;
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
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseCud<Ticket>, BaseCud<Ticket>>();
        services.AddScoped<IBaseCud<Attachment>, BaseCud<Attachment>>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
    {
        services.AddTransient<ITicketGet, TicketGet>();
        services.AddTransient<ITicketAction, TicketAction>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services)
    {
        services.AddFteamIdentityService();
        return Task.FromResult(services);
    }
}