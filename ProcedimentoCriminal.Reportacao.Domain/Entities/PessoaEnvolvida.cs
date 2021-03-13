using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class PessoaEnvolvida : Entity
    {
        public Envolvimento Envolvimento { get; private set; }
        public string Nome { get; private set; }
        public Identidade Identidade { get; private set; }
        public string NomeMae { get; private set; }
        public string NomePai { get; private set; }
        public Nascimento Nascimento { get; private set; }
        public string Cpf { get; private set; }
        public char? Sexo { get; private set; }
        public string Passaporte { get; private set; }
        public EstadoCivil? EstadoCivil { get; private set; }
        public GrauInstrucao? GrauInstrucao { get; private set; }
        public string NomeSocial { get; private set; }
        public Endereco EnderecoResidencial { get; private set; }
        public Endereco EnderecoComercial { get; private set; }
        private List<ObjetoEnvolvido> _objetosEnvolvidos;
        public ReadOnlyCollection<ObjetoEnvolvido> ObjetosEnvolvidos => _objetosEnvolvidos.AsReadOnly();
        private List<VeiculoEnvolvido> _veiculosEnvolvidos;

        public ReadOnlyCollection<VeiculoEnvolvido> VeiculosEnvolvidos
        {
            get => _veiculosEnvolvidos.AsReadOnly();
        }

        public PessoaEnvolvida(Envolvimento envolvimento, string nome, Identidade identidade, string nomeMae,
            string nomePai, Nascimento nascimento, string cpf, char? sexo, string passaporte, EstadoCivil? estadoCivil,
            GrauInstrucao? grauInstrucao, string nomeSocial, Endereco enderecoResidencial, Endereco enderecoComercial,
            List<ObjetoEnvolvido> objetosEnvolvidos, List<VeiculoEnvolvido> veiculosEnvolvidos)
        {
            Envolvimento = envolvimento;
            Nome = nome;
            Identidade = identidade;
            NomeMae = nomeMae;
            NomePai = nomePai;
            Nascimento = nascimento;
            Cpf = cpf;
            Sexo = sexo;
            Passaporte = passaporte;
            EstadoCivil = estadoCivil;
            GrauInstrucao = grauInstrucao;
            NomeSocial = nomeSocial;
            EnderecoResidencial = enderecoResidencial;
            EnderecoComercial = enderecoComercial;
            _objetosEnvolvidos = objetosEnvolvidos;
            _veiculosEnvolvidos = veiculosEnvolvidos;
        }
    }
}