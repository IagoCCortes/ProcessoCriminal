using System.ComponentModel;

namespace ProcedimentoCriminal.Reportacao.Domain.Enums
{
    public enum MeioEmpregado
    {
        [Description("Agressão Moral")] AgressaoMoral = 1,
        [Description("Ameaça")] Ameaca,
        [Description("Apropriação")] Apropriacao,
        [Description("Arma de Corte/Perfuração")] ArmaCorte,
        [Description("Arma de Fogo")] ArmaFogo,
        [Description("Arrombamento")] Arrombamento,
        [Description("Artefato Explosivo")] ArtefatoExplosivo,
        [Description("Clonagem de Dados")] ClonagemDados,
        [Description("Clonagem de Página")] ClonagemPagina,
        [Description("Corrente")] Corrente,
        [Description("Documentos Falsos")] DocumentosFalsos,
        [Description("Fio")] Fio,
        [Description("Fraude")] Fraude,
        [Description("Segmento de Madeira/Ferro")] SegmentoMadeiraFerro,
        [Description("Violência Física")] ViolenciaFisica,
        [Description("Violência Sexual")] ViolenciaSexual,
        [Description("Outros")] Outros,
        
    }
}