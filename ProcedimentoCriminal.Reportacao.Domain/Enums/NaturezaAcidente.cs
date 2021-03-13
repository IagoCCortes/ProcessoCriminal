using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum NaturezaAcidente
    {
        [Description("Abalroamento")] Abalroamento = 1,
        [Description("Atropelamento Animal")] AtropelamentoAnimal,
        [Description("Capotamento")] Capotamento,
        [Description("Choque com Obst.Fixo")] ChoqueComObstFixo,
        [Description("Colisão")] Colisao,
        [Description("Engavetamento")] Engavetamento,
        [Description("Queda de Veiculo")] QuedaVeiculo,
        [Description("Submersão")] Submersao,
        [Description("Tombamento")] Tombamento
    }
}