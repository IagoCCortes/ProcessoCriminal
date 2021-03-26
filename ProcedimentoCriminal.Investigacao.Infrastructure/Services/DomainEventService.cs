﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using ProcedimentoCriminal.Core.Application.Models;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Core.Domain.Interfaces;

namespace ProcedimentoCriminal.Investigacao.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IMediator _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}