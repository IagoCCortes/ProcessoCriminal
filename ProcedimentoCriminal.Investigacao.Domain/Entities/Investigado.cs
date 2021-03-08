using System;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Investigacao.Domain.Enums;
using ProcedimentoCriminal.Investigacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Investigacao.Domain.Entities
{
    public class Investigado : Entity
    {
        public string Nome { get; private set; }
        public string Rg { get; private set; }
        public string Cpf { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string Naturalidade { get; private set; }
        public string NomePai { get; private set; }
        public string NomeMae { get; private set; }
        public Telefone Telefone { get; private set; }
        public Endereco Endereco { get; private set; }
        public bool Indiciado { get; private set; }
        public SituacaoInvestigado Situacao { get; private set; }

        public Investigado(string nome, string rg, string cpf, DateTime? dataNascimento, string naturalidade,
            string nomePai, string nomeMae, Telefone telefone, Endereco endereco,
            SituacaoInvestigado situacao)
        {
            Nome = nome;
            Rg = rg;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Naturalidade = naturalidade;
            NomePai = nomePai;
            NomeMae = nomeMae;
            Telefone = telefone;
            Endereco = endereco;
            Indiciado = false;
            Situacao = situacao;
        }

        public void AlterarSituacaoInvestigado(SituacaoInvestigado situacao) => Situacao = situacao;

        public void IndiciarInvestigado()
        {
            Indiciado = true;
        }
    }
}