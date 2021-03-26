using System;
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
            builder.Property<DateTime>("created").IsRequired();
            builder.Property<string>("created_by").IsRequired();
            builder.Property<DateTime>("last_modified");
            builder.Property<string>("last_modified_by");
            return builder;
        }
    }
}