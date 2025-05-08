using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Tems.Exceptions;

public class UserAlreadyExists : Exception
{
    public UserAlreadyExists(string exception) : base(exception) { }
}
