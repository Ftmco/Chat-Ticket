namespace ChTi.DataBase.ViewModel;

public record ChatDetailViewModel(Guid Id, string Token, string Name, string Bio,
    string CreateDate, long LastMessageId, ChatTypeViewModel Type, IEnumerable<FileViewModel>? Avatars);

public enum ChatType
{
    Group = 0,
    Pv = 1
}

public record ChatTypeViewModel(string Type, short Code);