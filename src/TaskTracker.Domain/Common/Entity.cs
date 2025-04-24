using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Domain.Common;

public class Entity
{
    public int Id { get; init; }

    protected readonly List<IDomainEvent> _domainEvents = [];

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();

        _domainEvents.Clear();

        return copy;
    }

    protected Entity()
    {
    }
}
