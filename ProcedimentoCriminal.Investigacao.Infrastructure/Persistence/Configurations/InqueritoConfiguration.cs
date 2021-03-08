using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcedimentoCriminal.Investigacao.Domain.Entities;

namespace ProcedimentoCriminal.Investigacao.Infrastructure.Persistence.Configurations
{
    public class InqueritoConfiguration : IEntityTypeConfiguration<Inquerito>
    {
        public void Configure(EntityTypeBuilder<Inquerito> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Ignore(i => i.Anexos);
            builder.Ignore(i => i.Ocorrencias);
            builder.Ignore(i => i.IncidenciaPenal);
            builder.Ignore(i => i.Investigado);

            builder.Property(i => i.IdentificadorInquerito)
                .HasMaxLength(25)
                .HasColumnName("identificador_inquerito")
                .IsRequired();

            builder.ConfigureEntityProperties();
        }
    }
}