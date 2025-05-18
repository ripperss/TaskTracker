using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Infastructore.Exceptions;

public class NotFoundTeamByIdException : Exception
{
    public NotFoundTeamByIdException(string message) : base(message) { }
}
