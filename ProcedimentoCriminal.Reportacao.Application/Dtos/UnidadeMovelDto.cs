using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Dtos
{
    public class UnidadeMovelDto
    {
        public Orgao Orgao { get; set; }
        public string PrefixoVTR { get;  set; }
        public string Responsavel { get; set; }
        public string matriculaResponsavel { get; set; }
        public string UnidadeResponsavel { get; set; }
    }
}