using System;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FilterOcorrencias
{
    public class FilterOcorrenciasVm
    {
        public Guid Id { get; set; }
        public string IdentificadorOcorrencia { get; set; }
        public string Tipo { get; set; }
        public string DelegaciaPoliciaApuracao { get; set; }
        public string Natureza { get; set; }
        public DateTime DataHoraFato { get; set; }
        public DateTime DataHoraComunicacao { get; set; }
        public string EnderecoFato { get; set; }
        public bool PraticadoPorMenor { get; set; }
        public bool LocalPericiado { get; set; }
        public Guid? IdInquerito { get; set; }
        public string TipoLocal { get; set; }
        public string ObjetoMeioEmpregado { get; set; }
        
        public int PessoasEnvolvidas { get; set; }
        public int UnidadesMoveis { get; set; }
    }
}