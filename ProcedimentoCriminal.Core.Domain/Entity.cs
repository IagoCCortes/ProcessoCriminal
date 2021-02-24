using System;

namespace ProcedimentoCriminal.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public DateTime Created { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? LastModified { get; private set; }
        public string LastModifiedBy { get; private set; }

        public void Create(string createdBy)
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            CreatedBy = createdBy;
        }

        public void Update(string updatedBy)
        {
            LastModified = DateTime.Now;
            LastModifiedBy = updatedBy;
        }
        
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id={Id}]";
    }
}