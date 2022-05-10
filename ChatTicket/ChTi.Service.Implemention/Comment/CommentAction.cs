
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class CommentAction : ICommentAction
{
    readonly IBaseCud<Comment> _commentCud;

    public CommentAction(IBaseCud<Comment> commentCud)
    {
        _commentCud = commentCud;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public Task SendCommentAsync(SendCommentViewModel sendComment)
    {
        Comment comment = new()
        {
            CreateDate = DateTime.Now,
            IsActive = false,
            ObjectId = sendComment.ObjectId,
            ReplyId = sendComment.ReplyId,
            Text = sendComment.Text,
            UserId = Guid.NewGuid()
        };
        throw new NotImplementedException();
    }
}
