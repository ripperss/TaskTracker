using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
