using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Ocorrencia.Domain.ValueObjects;

namespace ProcedimentoCriminal.Ocorrencia.Domain.Entities
{
    public class Ocorrencia : Entity, IAggregateRoot
    {
        public Guid IdTipo { get; }
        public string DelegaciaPoliciaApuracao { get; }
        public Guid IdNatureza { get; }
        public DateTime DataHoraFato { get; }
        public DateTime DataHoraComunicacao { get; }
        public Endereco EnderecoFato { get; }
        public bool PraticadoPorMenor { get; }
        public bool LocalPericiado { get; }
        public Guid IdInquerito { get; private set; }
        public string TipoLocal { get; }
        public string ObjetoMeioEmpregado { get; }
        public List<PessoaEnvolvida> PessoasEnvolvidas { get; }
        public UnidadeMovel UnidadeMovel { get; }
        
        // EF Rels.
        public Natureza Natureza { get; private set; }
        public Tipo Tipo { get; private set; }
        

        public Ocorrencia(Guid idTipo, string delegaciaPoliciaApuracao, Guid idNatureza, DateTime dataHoraFato, 
            DateTime dataHoraComunicacao, Endereco enderecoFato, bool praticadoPorMenor, bool localPericiado, 
            string tipoLocal, string objetoMeioEmpregado, IEnumerable<PessoaEnvolvida> pessoasEnvolvidas, 
            UnidadeMovel unidadeMovel)
        {
            IdTipo = idTipo;
            DelegaciaPoliciaApuracao = delegaciaPoliciaApuracao;
            IdNatureza = idNatureza;
            DataHoraFato = dataHoraFato;
            DataHoraComunicacao = dataHoraComunicacao;
            EnderecoFato = enderecoFato;
            PraticadoPorMenor = praticadoPorMenor;
            LocalPericiado = localPericiado;
            TipoLocal = tipoLocal;
            ObjetoMeioEmpregado = objetoMeioEmpregado;
            PessoasEnvolvidas = new List<PessoaEnvolvida>(pessoasEnvolvidas);
            UnidadeMovel = unidadeMovel;
        }

        public void VincularPessoaEnvolvida(PessoaEnvolvida pessoa)
        {
            if (pessoa == null) throw new Exception();
            
            PessoasEnvolvidas.Add(pessoa);
        }

        public void VincularInquerito(Guid idInquerito)
        {
            if (idInquerito == Guid.Empty) throw new Exception(); 
            
            IdInquerito = idInquerito;
        }
    }
}