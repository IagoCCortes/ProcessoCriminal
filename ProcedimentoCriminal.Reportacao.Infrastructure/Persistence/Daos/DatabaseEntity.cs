using System;
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

        protected DatabaseEntity(Entity entity)
        {
            id = entity.Id;
            created = entity.Created;
            created_by = entity.CreatedBy;
            last_modified = entity.LastModified;
            last_modified_by = entity.LastModifiedBy;
        }
    }
}