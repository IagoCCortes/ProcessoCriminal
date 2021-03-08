using System.ComponentModel;

namespace ProcedimentoCriminal.Investigacao.Domain.Enums
{
    public enum SituacaoInvestigado
    {
        [Description("Preso em flagrante")] PresoEmFlagrante = 0,
        [Description("Foragido")] Foragido = 1,
    }
}