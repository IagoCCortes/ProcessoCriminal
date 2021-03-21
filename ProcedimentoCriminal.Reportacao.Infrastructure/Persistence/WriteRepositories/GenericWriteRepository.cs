using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories
{
    public abstract class GenericWriteRepository<T> : IGenericWriteRepository<T> where T : IAggregateRoot
    {
        private readonly IDapperConnectionFactory _connectionFactory;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        internal readonly List<(string sql, DatabaseEntity dbEntity, ChangeType changeType)> Changes;

        public GenericWriteRepository(IDapperConnectionFactory connectionFactory,
            IDomainEventService domainEventService,
            ICurrentUserService currentUserService, IDateTime dateTime)
        {
            _connectionFactory = connectionFactory;
            _domainEventService = domainEventService;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            Changes = new List<(string sql, DatabaseEntity dbEntity, ChangeType changeType)>();
        }

        public abstract void Insert(T aggregateRoot);

        public abstract void Delete(Guid id);

        public async Task<int> SaveChangesAsync()
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

                affectedRows += await connection.ExecuteAsync(change.sql, change.dbEntity, transaction);
            }

            transaction.Commit();

            await DispatchEvents();

            return affectedRows;
        }

        private async Task DispatchEvents()
        {
            var domainEvents = Changes.Where(c => c.dbEntity.HasDomainEvents())
                .SelectMany(c => c.dbEntity.DomainEvents());

            foreach (var domainEvent in domainEvents)
                await _domainEventService.Publish(domainEvent);
        }
    }
}