using ChTi.DataBase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Abstraction;

public interface ICommentAction : IAsyncDisposable
{
    Task SendCommentAsync(SendCommentViewModel sendComment);
}
