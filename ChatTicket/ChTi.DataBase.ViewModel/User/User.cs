namespace ChTi.DataBase.ViewModel;

public record UserViewModel(Guid Id, string FullName, string Email, string MobileNo, IEnumerable<FileViewModel>? Avatar);