using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOcorrenciaById
{
    public class FetchOcorrenciaByIdVm
    {
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
        public List<FetchOcorrenciaByIdPessoaEnvolvida> PessoasEnvolvidas { get; set; }
        public List<FetchOcorrenciaByIdUnidadeMovel> UnidadesMoveis { get; set; }
    }

    public class FetchOcorrenciaByIdPessoaEnvolvida
    {
        public string Nome { get; set; }
        public string Envolvimento { get; set; }
        public char Sexo { get; set; }
        public string CPF { get; set; }
        public string Profissao { get; set; }
        public string GravidadeLesoes { get; set; }
        public string RacaCor { get; set; }
    }

    public class FetchOcorrenciaByIdUnidadeMovel
    {
        public string Orgao { get; set; }
        public string PrefixoVtr { get;  set; }
        public string Responsavel { get; set; }
        public string matriculaResponsavel { get; set; }
        public string UnidadeResponsavel { get; set; }
    }
}