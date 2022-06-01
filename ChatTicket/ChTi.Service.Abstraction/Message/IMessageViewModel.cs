using Identity.Client.Models;

namespace ChTi.Service.Abstraction;

public interface IMessageViewModel : IAsyncDisposable
{
    Task<IEnumerable<MessageViewModel>> CreateMessageViewModelAsync(IEnumerable<Message> messages, User clientUser);

    Task<MessageViewModel> CreateMessageViewModelAsync(Message message,User clientUser);
}
