using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Tems.Exceptions;

public class IncorrectPasswordExcepton : Exception
{
    public IncorrectPasswordExcepton(string exception) : base(exception) { }
}
