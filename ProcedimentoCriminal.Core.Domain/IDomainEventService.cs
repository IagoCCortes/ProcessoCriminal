using System.Threading.Tasks;

namespace ProcedimentoCriminal.Core.Domain
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
