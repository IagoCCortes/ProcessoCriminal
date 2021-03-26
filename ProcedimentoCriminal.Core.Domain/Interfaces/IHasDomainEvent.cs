using System.Collections.Generic;

namespace ProcedimentoCriminal.Core.Domain.Interfaces
{
    public interface IHasDomainEvent
    {
        List<DomainEvent> DomainEvents { get; }
    }
}