using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Investigacao.Domain.Events;
using ProcedimentoCriminal.Investigacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Investigacao.Domain.Entities
{
    public class Inquerito : Entity, IAggregateRoot, IHasDomainEvent
    {
        public IdentificadorInquerito IdentificadorInquerito { get; private set; }
        public Investigado Investigado { get; private set; }
        public List<string> IncidenciaPenal { get; private set; }
        public Endereco LocalInfracao { get; private set; }
        public List<IAnexo> Anexos { get; private set; }
        public List<Guid> Ocorrencias { get; private set; }
        public List<DomainEvent> DomainEvents { get; }

        private Inquerito(IdentificadorInquerito identificadorInquerito, Investigado investigado,
            List<string> incidenciaPenal,
            Endereco localInfracao)
        {
            IdentificadorInquerito = identificadorInquerito;
            Investigado = investigado;
            IncidenciaPenal = incidenciaPenal;
            LocalInfracao = localInfracao;

            DomainEvents = new List<DomainEvent>();
        }

        public void InstaurarInquerito(IdentificadorInquerito identificadorInquerito, Investigado investigado,
            List<string> incidenciaPenal,
            Endereco localInfracao)
        {
            var inquerito = new Inquerito(identificadorInquerito, investigado, incidenciaPenal, localInfracao);
            DomainEvents.Add(new InqueritoCriadoEvent(this));
        }

        public void VincularAnexo(IAnexo anexo)
        {
            if (anexo == null) throw new DomainException("Nenhum anexo fornecido");

            Anexos.Add(anexo);
        }

        public void SolicitarPericia()
        {
            // Disparar evento
        }

        public void VincularOcorrencia(Guid idOcorrencia)
        {
            if (idOcorrencia == Guid.Empty) throw new DomainException("Nenhum identificador de Ocorrência passado");

            Ocorrencias.Add(idOcorrencia);
        }
    }

    public interface IAnexo
    {
    }

    public class NotaCulpa : IAnexo
    {
    }

    public class TermoRestituicao : IAnexo
    {
    }

    public class AutoApreensao : IAnexo
    {
    }
}