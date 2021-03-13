using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum EstadoCivil
    {
        [Description("Amasiado(a)")] Amasiado = 1,
        [Description("Casado(a)")] Casado = 2,
        [Description("Convivente")] Convivente = 3,
        [Description("Desquitado(a)")] Desquitado = 4,
        [Description("Divorciado(a)")] Divorciado = 5,
        [Description("Não Informado")] NaoInformado = 6,

        [Description("Separado(a) Consesualmente")]
        SeparadoConsesualmente = 7,

        [Description("Separado(a) Judicialmente")]
        SeparadoJudicialmente = 8,
        [Description("Solteiro(a)")] Solteiro = 9,
        [Description("Viúvo(a)")] Viuvo = 10,
    }
}