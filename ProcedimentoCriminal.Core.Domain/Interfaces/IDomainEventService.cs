using System.Threading.Tasks;

namespace ProcedimentoCriminal.Core.Domain.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
