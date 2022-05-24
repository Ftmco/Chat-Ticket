using Microsoft.AspNetCore.Http;

namespace ChTi.Service.Abstraction;

public interface IMessageAction : IAsyncDisposable
{
    Task<SendMessageResponse> SendMessageAsync(SendMessageViewModel sendMessage, IHeaderDictionary headers);
}
