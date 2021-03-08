using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Investigacao.Domain.Entities;

namespace ProcedimentoCriminal.Investigacao.Domain.Events
{
    public class InqueritoCriadoEvent : DomainEvent
    {
        public Inquerito Inquerito { get; private set; }

        public InqueritoCriadoEvent(Inquerito inquerito) => Inquerito = inquerito;
    }
}