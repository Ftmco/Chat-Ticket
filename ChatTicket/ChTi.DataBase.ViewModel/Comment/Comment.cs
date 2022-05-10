using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.DataBase.ViewModel;

public record CommentViewModel(Guid Id, Guid? ReplyId, string ObjectId, string Text);

public record SendCommentViewModel(string ObjectId, string Text, Guid? ReplyId);