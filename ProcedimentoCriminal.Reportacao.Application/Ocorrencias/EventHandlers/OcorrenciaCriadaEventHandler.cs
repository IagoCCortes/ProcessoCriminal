using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ProcedimentoCriminal.Core.Application.Models;
using ProcedimentoCriminal.Reportacao.Domain.Events;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.EventHandlers
{
    public class OcorrenciaCriadaEventHandler : INotificationHandler<DomainEventNotification<OcorrenciaCriadaEvent>>
    {
        private readonly ILogger<OcorrenciaCriadaEvent> _logger;

        public OcorrenciaCriadaEventHandler(ILogger<OcorrenciaCriadaEvent> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<OcorrenciaCriadaEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            
            // Oficiar ocorrência

            _logger.LogInformation($"CleanArchitecture Domain Event: {domainEvent.GetType().Name}");

            return Task.CompletedTask;
        }
    }
}