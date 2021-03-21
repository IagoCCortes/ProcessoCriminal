using System;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos
{
    [TableName("ocorrencias")]
    public class OcorrenciaDao : DatabaseEntity
    {
        public string identificador_ocorrencia { get; private set; }
        public string delegacia_policia_apuracao { get; private set; }
        public int id_natureza { get; private set; }
        public DateTime inicio_periodo_fato { get; private set; }
        public DateTime fim_periodo_fato { get; private set; }
        public DateTime data_hora_comunicacao { get; private set; }
        public string local_fato { get; private set; }
        public string descricao_fato { get; private set; }

        public OcorrenciaDao(Guid id) : base(id)
        {
        }

        public OcorrenciaDao(Ocorrencia ocorrencia) : base(ocorrencia)
        {
            identificador_ocorrencia = ocorrencia.IdentificadorOcorrencia.ToString();
            delegacia_policia_apuracao = ocorrencia.DelegaciaPoliciaApuracao.ToString();
            id_natureza = (int) ocorrencia.Natureza;
            inicio_periodo_fato = ocorrencia.PeriodoFato.Inicio;
            fim_periodo_fato = ocorrencia.PeriodoFato.Fim;
            data_hora_comunicacao = ocorrencia.DataHoraComunicacao;
            local_fato = ocorrencia.LocalFato.ToString();
            descricao_fato = ocorrencia.DescricaoFato;
        }
    }
}