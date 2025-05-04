using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Infastructore.Auth;

public class JwtSettings
{
    public string? TokenPrivateKey { get; set; }
    public int Expires { get; set; }
}
