using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class Ocorrencia : Entity, IAggregateRoot
    {
        public string IdentificadorOcorrencia { get; private set; }
        public Tipo Tipo { get; }
        public string DelegaciaPoliciaApuracao { get; }
        public Natureza Natureza { get; }
        public DateTime DataHoraFato { get; }
        public DateTime DataHoraComunicacao { get; }
        public Endereco EnderecoFato { get; }
        public bool PraticadoPorMenor { get; }
        public bool LocalPericiado { get; }
        public Guid IdInquerito { get; private set; }
        public string TipoLocal { get; }
        public string ObjetoMeioEmpregado { get; }
        public List<PessoaEnvolvida> PessoasEnvolvidas { get; }
        public UnidadeMovel UnidadeMovel { get; private set; }

        public Ocorrencia(string identificadorOcorrencia, Tipo tipo, string delegaciaPoliciaApuracao, Natureza natureza, DateTime dataHoraFato, 
            DateTime dataHoraComunicacao, Endereco enderecoFato, bool praticadoPorMenor, bool localPericiado, 
            string tipoLocal, string objetoMeioEmpregado)
        {
            IdentificadorOcorrencia = identificadorOcorrencia;
            Tipo = tipo;
            DelegaciaPoliciaApuracao = delegaciaPoliciaApuracao;
            Natureza = natureza;
            DataHoraFato = dataHoraFato;
            DataHoraComunicacao = dataHoraComunicacao;
            EnderecoFato = enderecoFato;
            PraticadoPorMenor = praticadoPorMenor;
            LocalPericiado = localPericiado;
            TipoLocal = tipoLocal;
            ObjetoMeioEmpregado = objetoMeioEmpregado;
            PessoasEnvolvidas = new List<PessoaEnvolvida>();
        }

        public void VincularPessoaEnvolvida(PessoaEnvolvida pessoa)
        {
            if (pessoa == null) throw new DomainException("Nenhuma pessoa foi passada para vinculação");
            
            PessoasEnvolvidas.Add(pessoa);
        }

        public void VincularInquerito(Guid idInquerito)
        {
            if (idInquerito == Guid.Empty) throw new DomainException("Nenhum identificador de Inquérito passado"); 
            
            IdInquerito = idInquerito;
        }

        public void VincularUnidadeMovel(UnidadeMovel unidadeMovel)
        {
            if (unidadeMovel == null) throw new DomainException("Nenhuma unidade móvel foi passada para vinculação");

            UnidadeMovel = unidadeMovel;
        }
    }
}