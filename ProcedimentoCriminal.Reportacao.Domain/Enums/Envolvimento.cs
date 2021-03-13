using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum Envolvimento
    {
        [Description("Comunicante/Vítima")]ComunicanteVitima = 1,
        [Description("Comunicante")]Comunicante = 2,
        [Description("Testemunha")]Testemunha = 3,
        [Description("Vítima")]Vitima = 4,
        [Description("Autor")]Autor = 5,
        }
}