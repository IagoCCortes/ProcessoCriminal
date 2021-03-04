using System;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class PessoaEnvolvida : Entity
    {
        public string Nome { get; private set; }
        public string Envolvimento { get; private set; }
        public char Sexo { get; private set; }
        public string CPF { get; private set; }
        public string Profissao { get; private set; }
        public string GravidadeLesoes { get; private set; }
        public string RacaCor { get; private set; }
        public Guid IdOcorrencia { get; private set; }

        public PessoaEnvolvida(string nome, string envolvimento, char sexo, string cpf, string profissao,
            string gravidadeLesoes, string racaCor) : base()
        {
            Nome = nome;
            Envolvimento = envolvimento;
            Sexo = sexo;
            CPF = cpf;
            Profissao = profissao;
            GravidadeLesoes = gravidadeLesoes;
            RacaCor = racaCor;
        }
    }
}