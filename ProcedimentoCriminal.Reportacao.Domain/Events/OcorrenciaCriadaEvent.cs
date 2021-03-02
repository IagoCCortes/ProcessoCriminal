using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.Events
{
    public class OcorrenciaCriadaEvent : DomainEvent
    {
        public string Descricao { get; }

        public OcorrenciaCriadaEvent(string descricao)
        {
            Descricao = descricao;
        }
    }
}