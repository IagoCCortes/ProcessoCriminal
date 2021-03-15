using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos
{
    public abstract class DatabaseEntity
    {
        public Guid id { get; set; }
        public DateTime created { get; set; }
        public string created_by { get; set; }
        public DateTime? last_modified { get; set; }
        public string last_modified_by { get; set; }
        public List<DomainEvent> DomainEvents { get; set; }

        protected DatabaseEntity(Entity entity)
        {
            id = entity.Id;
            DomainEvents = new List<DomainEvent>();
            if (entity is IHasDomainEvent @hasDomainEvents)
                DomainEvents.AddRange(@hasDomainEvents.DomainEvents);
        }
    }
}