namespace ChTi.DataBase.ViewModel;

public record SendMessageViewModel(string ChatToken, string Text, long? ReplyMessageId, Guid? FileId, string? FileToken);

public record MessageViewModel(Guid Id, long MessageId, string? ChatToken,
    string Text, string SendDate, string SendTime,
    long? ReplyMessageId, long ReplyCount, UserViewModel? User);

public record SendMessageResponse(MessageActionStatus Status, MessageViewModel? Message);


public enum MessageActionStatus
{
    Success = 0,
    AccessDenied = 1,
    Exception = 2,
    MessageNotFound = 3,
    ChatNotFound = 4
}