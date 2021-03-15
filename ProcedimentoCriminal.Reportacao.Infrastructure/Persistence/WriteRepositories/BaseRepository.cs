using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories
{
    public abstract class BaseRepository
    {
        private readonly IDapperConnectionFactory _connectionFactory;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        protected readonly List<(DatabaseEntity dbEntity, ChangeType changeType)> Changes;

        public BaseRepository(IDapperConnectionFactory connectionFactory, IDomainEventService domainEventService,
            ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _connectionFactory = connectionFactory;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            Changes = new List<(DatabaseEntity dbEntity, ChangeType changeType)>();
        }

        protected async Task<int> SaveChangesAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var transaction = connection.BeginTransaction();
            var affectedRows = 0;

            foreach (var change in Changes)
            {
                switch (change.changeType)
                {
                    case ChangeType.INSERT:
                        change.dbEntity.created = _dateTime.UtcNow;
                        change.dbEntity.created_by = _currentUserService.UserId;
                        break;
                    case ChangeType.UPDATE:
                        change.dbEntity.last_modified = _dateTime.UtcNow;
                        change.dbEntity.last_modified_by = _currentUserService.UserId;
                        break;
                }

                affectedRows += await connection.ExecuteAsync(change.dbEntity.BuildInsertStatement(), change.dbEntity,
                    transaction: transaction);
            }


            transaction.Commit();

            await DispatchEvents();

            return affectedRows;
        }

        protected async Task DispatchEvents()
        {
            var domainEvents = Changes.Where(c => c.dbEntity.DomainEvents.Any())
                .SelectMany(c => c.dbEntity.DomainEvents);

            foreach (var domainEvent in domainEvents)
                await _domainEventService.Publish(domainEvent);
        }
    }
}