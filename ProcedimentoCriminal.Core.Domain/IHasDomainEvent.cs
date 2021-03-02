using System.Collections.Generic;

namespace ProcedimentoCriminal.Core.Domain
{
    public interface IHasDomainEvent
    {
        List<DomainEvent> DomainEvents { get; }
    }
}