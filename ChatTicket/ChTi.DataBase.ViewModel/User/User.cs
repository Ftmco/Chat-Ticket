using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.ViewModel;

public record UserViewModel(Guid Id, string FullName, string Email, string MobileNo, IEnumerable<FileViewModel>? Avatar);