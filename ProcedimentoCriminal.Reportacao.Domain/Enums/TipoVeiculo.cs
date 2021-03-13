using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum TipoVeiculo
    {
        [Description("Automóvel")] Automovel = 1,
        [Description("Caminhão")] Caminhao,
        [Description("Caminhonete")] Caminhonete,
        [Description("Motocicleta")] Motocicleta,
        [Description("Ônibus")] Onibus,
        [Description("Reboque")] Reboque,
        [Description("Triciclo")] Triciclo,
    }
}