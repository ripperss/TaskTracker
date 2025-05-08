using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Tems.Exceptions;

public class NoPermissionException : Exception
{
    public NoPermissionException(string  message) : base(message) { }
}
