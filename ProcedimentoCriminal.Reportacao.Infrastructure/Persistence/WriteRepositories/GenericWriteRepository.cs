using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Core.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories
{
    public abstract class GenericWriteRepository<T> : IGenericWriteRepository<T> where T : IAggregateRoot
    {
        protected readonly List<(string sql, DatabaseEntity dbEntity, ChangeType changeType)> Changes;

        protected GenericWriteRepository(List<(string sql, DatabaseEntity dbEntity, ChangeType changeType)> changes)
        {
            Changes = changes;
        }
        
        public abstract void Insert(T aggregateRoot);

        public abstract void Delete(Guid id);
    }
}