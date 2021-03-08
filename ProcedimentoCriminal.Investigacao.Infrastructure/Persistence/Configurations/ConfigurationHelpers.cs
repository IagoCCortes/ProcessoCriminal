using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Investigacao.Infrastructure.Persistence.Configurations
{
    public static class ConfigurationHelpers
    {
        public static EntityTypeBuilder<T> ConfigureEntityProperties<T>(this EntityTypeBuilder<T> builder) where T : Entity
        {
            builder.Property(e => e.Id).HasColumnName("id").IsRequired();
            builder.Property(e => e.Created).HasColumnName("created").IsRequired();
            builder.Property(e => e.CreatedBy).HasColumnName("created_by").IsRequired();
            builder.Property(e => e.LastModified).HasColumnName("last_modified").IsRequired();
            builder.Property(e => e.LastModifiedBy).HasColumnName("last_modified_by").IsRequired();
            return builder;
        }
    }
}