using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.Events;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class Ocorrencia : Entity, IAggregateRoot, IHasDomainEvent
    {
        public string IdentificadorOcorrencia { get; }
        public Tipo Tipo { get; }
        public string DelegaciaPoliciaApuracao { get; }
        public Natureza Natureza { get; }
        public DateTime DataHoraFato { get; }
        public DateTime DataHoraComunicacao { get; }
        public Endereco EnderecoFato { get; }
        public bool PraticadoPorMenor { get; }
        public bool LocalPericiado { get; }
        public Guid? IdInquerito { get; private set; }
        public string TipoLocal { get; }
        public string ObjetoMeioEmpregado { get; }
        public List<PessoaEnvolvida> PessoasEnvolvidas { get; }
        public List<UnidadeMovel> UnidadesMoveis { get; }

        public Ocorrencia(string identificadorOcorrencia, Tipo tipo, string delegaciaPoliciaApuracao, Natureza natureza,
            DateTime dataHoraFato, DateTime dataHoraComunicacao, Endereco enderecoFato, bool praticadoPorMenor,
            bool localPericiado, string tipoLocal, string objetoMeioEmpregado,
            List<PessoaEnvolvida> pessoasEnvolvidas, List<UnidadeMovel> unidadesMoveis) : base()
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
            PessoasEnvolvidas = pessoasEnvolvidas;
            UnidadesMoveis = unidadesMoveis;

            DomainEvents = new List<DomainEvent> {new OcorrenciaCriadaEvent("Test")};
        }

        public void VincularInquerito(Guid idInquerito)
        {
            if (idInquerito == Guid.Empty) throw new DomainException("Nenhum identificador de Inquérito passado");

            IdInquerito = idInquerito;
        }

        public List<DomainEvent> DomainEvents { get; }
    }
}