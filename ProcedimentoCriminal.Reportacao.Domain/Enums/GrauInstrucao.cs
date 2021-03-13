using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum GrauInstrucao
    {
        [Description("Básico")] Basico = 1,
        [Description("Básico Incompleto")] BasicoIncompleto = 2,
        [Description("Doutorado Completo")] DoutoradoCompleto = 3,
        [Description("Doutorado Incompleto")] DoutoradoIncompleto = 4,
        [Description("Fundamental")] Fundamental = 5,

        [Description("Fundamental Incompleto")]
        FundamentalIncompleto = 6,
        [Description("Médio")] Medio = 7,
        [Description("Médio Incompleto")] MedioIncompleto = 8,
        [Description("Mestrado Completo")] MestradoCompleto = 9,
        [Description("Mestrado Incompleto")] MestradoIncompleto = 10,
        [Description("Não Alfabetizado")] NaoAlfabetizado = 11,
        [Description("Não Informado")] NaoInformado = 12,
        [Description("Pós-Graduado")] PosGraduado = 13,
        [Description("Primário")] Primario = 14,
        [Description("Superior")] Superior = 15,
        [Description("Superior Incompleto")] SuperiorIncompleto = 16,
    }
}