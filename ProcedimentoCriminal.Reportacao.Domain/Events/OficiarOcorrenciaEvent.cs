using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.Events
{
    public class OficiarOcorrenciaEvent : DomainEvent
    {
        public string Descricao { get; }

        public OficiarOcorrenciaEvent(string descricao)
        {
            Descricao = descricao;
        }
    }
}