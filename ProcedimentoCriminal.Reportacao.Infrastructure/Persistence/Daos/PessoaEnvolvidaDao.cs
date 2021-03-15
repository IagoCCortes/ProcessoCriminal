using System;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos
{
    [TableName("pessoas_envolvidas")]
    public class PessoaEnvolvidaDao : DatabaseEntity
    {
        public int? id_envolvimento { get; private set; }
        public string nome { get; private set; }
        public int? identidade_rg { get; private set; }
        public string identidade_orgao_emissor { get; private set; }
        public int? id_identidade_uf { get; private set; }
        public string nome_mae { get; private set; }
        public string nome_pai { get; private set; }
        public DateTime? nascimento_data { get; private set; }
        public int? id_nascimento_uf { get; private set; }
        public string cpf { get; private set; }
        public char? sexo { get; private set; }
        public string passaporte { get; private set; }
        public int? id_estado_civil { get; private set; }
        public int? id_grau_instrucao { get; private set; }
        public string nome_social { get; private set; }
        public string endereco_residencial { get; private set; }
        public string endereco_comercial { get; private set; }
        public Guid id_ocorrencia { get; private set; }

        public PessoaEnvolvidaDao(PessoaEnvolvida entity, Guid idOcorrencia) : base(entity)
        {
            id_envolvimento = (int) entity.Envolvimento;
            nome = entity.Nome;
            identidade_rg = entity.Identidade?.Rg;
            identidade_orgao_emissor = entity.Identidade?.OrgaoEmissor;
            id_identidade_uf = (int?) entity.Identidade?.Uf;
            nome_mae = entity.NomeMae;
            nome_pai = entity.NomePai;
            nascimento_data = entity.Nascimento?.Data;
            id_nascimento_uf = (int?) entity.Nascimento?.Uf;
            cpf = entity.Cpf;
            sexo = entity.Sexo;
            passaporte = entity.Passaporte;
            id_estado_civil = (int?) entity.EstadoCivil;
            id_grau_instrucao = (int?) entity.GrauInstrucao;
            nome_social = entity.NomeSocial;
            endereco_residencial = entity.EnderecoResidencial?.ToString();
            endereco_comercial = entity.EnderecoComercial?.ToString();
            id_ocorrencia = idOcorrencia;
        }
    }
}