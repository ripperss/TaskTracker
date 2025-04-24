using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.TasksUser;

public class CommentOverflowException : Exception
{
    public CommentOverflowException() : base("Превышено максимальное количество комментариев для задачи") { }

    public CommentOverflowException(string message) : base(message) { }
}
