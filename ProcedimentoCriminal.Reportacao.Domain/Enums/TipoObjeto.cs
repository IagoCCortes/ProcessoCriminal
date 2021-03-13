using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum TipoObjeto
    {
        [Description("Animal")] Animal = 1,
        [Description("Bagagem")] Bagagem,
        [Description("Bicicleta")] Bicicleta,
        [Description("Cartão")] Cartao,
        [Description("Jóias")] Joias,
        [Description("Outros")] Outros,
        [Description("Valores")] Valores
    }
}