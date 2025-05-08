using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Tems.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string exception) : base(exception) { }
}
