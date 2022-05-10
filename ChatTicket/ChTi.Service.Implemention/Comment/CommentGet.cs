using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChTi.Service.Implemention;

public class CommentGet : ICommentGet
{
    readonly IBaseQuery<Comment> _commentQuery;

    public CommentGet(IBaseQuery<Comment> commentQuery)
    {
        _commentQuery = commentQuery;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
