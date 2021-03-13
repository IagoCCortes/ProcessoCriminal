using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum Natureza
    {
        [Description("Violência Doméstica Contra Mulher")] ViolenciaContraMulher = 1,
        [Description("Perturbações")] Perturbacoes = 2,
        [Description("Acidente de Trânsito sem Vítima")] AcidenteTransitoSemVitimas = 3,
        [Description("Ofensas - Injúria, Calúnia ou Difamação")] Ofensas = 4,
        [Description("Ofensas Raciais")] OfensaRacial = 5,
        [Description("Ameaça")] Ameaca = 6,
        [Description("Furtos")] Furtos = 7,
        [Description("Estelionato, Fraudes e Apropriações")] EstelionatoFraudesApropriacoes = 8,
        [Description("Extravio / Perda")] ExtravioPerda = 9,
        [Description("Maus-tratos aos animais")] MausTratosAnimais = 10,
    }
}