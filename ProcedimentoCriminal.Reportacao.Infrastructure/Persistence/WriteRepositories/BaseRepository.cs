using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories
{
    public abstract class BaseRepository
    {
        protected readonly IDapperConnectionFactory ConnectionFactory;
        protected readonly IDomainEventService DomainEventService;
        protected readonly ICurrentUserService CurrentUserService;

        public BaseRepository(IDapperConnectionFactory connectionFactory, IDomainEventService domainEventService, ICurrentUserService currentUserService)
        {
            ConnectionFactory = connectionFactory;
            DomainEventService = domainEventService;
            CurrentUserService = currentUserService;
        }

        protected async Task<int> ExecuteTransactionAsync(List<Func<IDbConnection, IDbTransaction, Task<int>>> operations)
        {
            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();
            var affectedRows = 0;

            foreach (var operation in operations)
                affectedRows += await operation(connection, transaction);
            
            transaction.Commit();

            return affectedRows;
        }
        
        protected async Task DispatchEvents(List<DomainEvent> events)
        {
            foreach (var domainEvent in events)
                await DomainEventService.Publish(domainEvent);
        }
    }
}