using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<DomainEvent> _domainEvents;

        protected DatabaseEntity(Guid id)
        {
            this.id = id;
            _domainEvents = new List<DomainEvent>();
        }

        protected DatabaseEntity(Entity entity) : this(entity.Id)
        {
            if (entity is IHasDomainEvent hasDomainEvents)
                _domainEvents.AddRange(hasDomainEvents.DomainEvents);
        }

        public bool HasDomainEvents() => _domainEvents.Any();
        public List<DomainEvent> DomainEvents() => _domainEvents;
    }
}