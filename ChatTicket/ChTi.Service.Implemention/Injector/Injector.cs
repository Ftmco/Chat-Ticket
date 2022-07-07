using ChTi.DataBase.Context;
using ChTi.Service.Implemention.Base;
using Identity.Client.StartUp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ChTi.Service.Implemention.Injector;

public static class Injector
{
    public static async Task<IServiceCollection> AddChTiServicesAsync(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddSingleton<IMongoClient>((service) =>
        //{
        //    string? connectionString = configuration.GetConnectionString("ChTi_Db");
        //    return new MongoClient(connectionString);
        //});
        services.AddDbContext<ChatContext>(options =>
        {
            var cnn = configuration.GetConnectionString("Chat_Db");
            ChatContext.ConnectionString = cnn;
            options.UseNpgsql(cnn);
        });
        services.AddDbContext<TicketContext>(options =>
        {
            var cnn = configuration.GetConnectionString("Ticket_Db");
            TicketContext.ConnectionString = cnn;
            options.UseNpgsql(cnn);
        });
       
        await services.AddBaseCudAsync();
        await services.AddBaseQueryAsync();
        await services.AddServicesAsync();
        await services.AddToolsAsync();
        return services;
    }

    public static Task<IServiceCollection> AddBaseQueryAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseQuery<Ticket, TicketContext>, BaseQuery<Ticket, TicketContext>>();
        services.AddScoped<IBaseQuery<Attachment, TicketContext>, BaseQuery<Attachment, TicketContext>>();
        services.AddScoped<IBaseQuery<Attachment, ChatContext>, BaseQuery<Attachment, ChatContext>>();
        services.AddScoped<IBaseQuery<GroupChat, ChatContext>, BaseQuery<GroupChat, ChatContext>>();
        services.AddScoped<IBaseQuery<PvChat, ChatContext>, BaseQuery<PvChat, ChatContext>>();
        services.AddScoped<IBaseQuery<ChannelChat, ChatContext>, BaseQuery<ChannelChat, ChatContext>>();
        services.AddScoped<IBaseQuery<ChatsUsers, ChatContext>, BaseQuery<ChatsUsers, ChatContext>>();
        services.AddScoped<IBaseQuery<Message, ChatContext>, BaseQuery<Message, ChatContext>>();
        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddBaseCudAsync(this IServiceCollection services)
    {
        services.AddScoped<IBaseCud<Ticket, TicketContext>, BaseCud<Ticket, TicketContext>>();
        services.AddScoped<IBaseCud<Attachment, TicketContext>, BaseCud<Attachment, TicketContext>>();
        services.AddScoped<IBaseCud<Attachment, ChatContext>, BaseCud<Attachment, ChatContext>>();
        services.AddScoped<IBaseCud<GroupChat, ChatContext>, BaseCud<GroupChat, ChatContext>>();
        services.AddScoped<IBaseCud<PvChat, ChatContext>, BaseCud<PvChat, ChatContext>>();
        services.AddScoped<IBaseCud<ChannelChat, ChatContext>, BaseCud<ChannelChat, ChatContext>>();
        services.AddScoped<IBaseCud<ChatsUsers, ChatContext>, BaseCud<ChatsUsers, ChatContext>>();
        services.AddScoped<IBaseCud<Message, ChatContext>, BaseCud<Message, ChatContext>>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddServicesAsync(this IServiceCollection services)
    {
        services.AddTransient<ITicketGet, TicketGet>();
        services.AddTransient<ITicketAction, TicketAction>();
        services.AddTransient<ITicketViewModel, TicketViewModelService>();

        services.AddTransient<IChatGet, ChatGet>();
        services.AddTransient<IChatAction, ChatAction>();
        services.AddTransient<IChatViewModel, ChatViewModelService>();

        services.AddTransient<IMessageGet, MessageGet>();
        services.AddTransient<IMessageAction, MessageAction>();
        services.AddTransient<IMessageViewModel, MessageViewModelService>();

        //services.AddTransient<IMessageGet, MessageGet>();
        services.AddTransient<IChatUserAction, ChatUserAction>();
        //services.AddTransient<IMessageViewModel, MessageViewModelService>();

        return Task.FromResult(services);
    }

    public static Task<IServiceCollection> AddToolsAsync(this IServiceCollection services)
    {
        services.AddFteamIdentityService();
        return Task.FromResult(services);
    }
}