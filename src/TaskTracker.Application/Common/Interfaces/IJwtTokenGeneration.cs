using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Common.Interfaces;

public interface IJwtTokenGeneration
{
    Task<string> GenerationJwtToken(User user);
}
