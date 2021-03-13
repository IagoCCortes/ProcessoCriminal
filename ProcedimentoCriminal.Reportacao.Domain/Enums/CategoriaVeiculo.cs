using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum CategoriaVeiculo
    {
        [Description("Aluguel")] Aluguel = 1,
        [Description("Escolar")] Escolar,
        [Description("Não Informado")] NaoInformado,
        [Description("Particular")] Particular,
        [Description("Táxi")] Taxi,
    }
}