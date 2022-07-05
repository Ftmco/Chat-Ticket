namespace ChTi.DataBase.ViewModel;

public record ChatDetailViewModel(Guid Id, string Token, string Name, string Description,
    string CreateDate, string UpdateDate, long LastMessageId,
    long MessageCount, ChatTypeViewModel Type, IEnumerable<FileViewModel>? Avatars);

public record PvChatDetailViewModel(Guid Id, string Token, string Name, string MobileNo, string Email,
    string CreateDate, string UpdateDate, long LastMessageId, long MessageCount, IEnumerable<FileViewModel>? Avatars);

public record UpsertChatViewModel(Guid? Id, string Name, string Description, ChatType Type);

public record UpsertChatResponse(ChatActionStatus Status, ChatDetailViewModel? Chat);

public record ChatTypeViewModel(string Type, short Code);

public record AddUserToChatViewModel(Guid? ChatId, Guid UserId, ChatUserType UserType);

public record PvChatResponse(ChatActionStatus Status, PvChatDetailViewModel? Chat);

public enum ChatType
{
    Group = 0,
    Pv = 1
}

public enum ChatStatus
{
    Active = 0,
    Deleted = 1
}

public enum ChatActionStatus
{
    Success = 0,
    UserNotAuthorized = 1,
    Exception = 2,
    ChatNotFound = 3,
    AccessDenied = 4,
    UserNotFound = 5
}

public enum ChatUserType
{
    Owner = 0,
    Admin = 1,
    User = 2
}

