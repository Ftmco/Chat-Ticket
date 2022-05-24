namespace ChTi.Service.Abstraction;

public interface IMessageViewModel : IAsyncDisposable
{
    Task<IEnumerable<MessageViewModel>> CreateMessageViewModelAsync(IEnumerable<Message> messages);

    Task<MessageViewModel> CreateMessageViewModelAsync(Message message);
}
