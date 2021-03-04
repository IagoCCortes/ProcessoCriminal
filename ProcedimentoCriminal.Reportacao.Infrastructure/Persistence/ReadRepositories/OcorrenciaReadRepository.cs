using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ProcedimentoCriminal.Reportacao.Application.Interfaces.ReadRepositories;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOcorrenciaById;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FilterOcorrencias;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.ReadRepositories
{
    public class OcorrenciaReadRepository : IOcorrenciaReadRepository
    {
        private readonly DapperConnectionFactory _connectionFactory;

        public OcorrenciaReadRepository(DapperConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<FilterOcorrenciasVm>> FilterOcorrenciasAsync(FilterOcorrenciasQuery query)
        {
            query.PrepareStringsForLikeOperation();
            var sql = new StringBuilder(
                "SELECT DISTINCT o.id, identificador_ocorrencia as IdentificadorOcorrencia, tipo as Tipo, " +
                "delegacia_policia_apuracao as DelegaciaPoliciaApuracao, natureza as Natureza, data_hora_fato as DataHoraFato, " +
                "data_hora_comunicacao as DataHoraComunicacao, endereco_fato as EnderecoFato, praticado_por_menor as PraticadoPorMenor, " +
                "local_periciado as LocalPericiado, tipo_local as TipoLocal, objeto_meio_empregado as ObjetoMeioEmpregado, " +
                "o.created as Created, o.created_by as CreatedBy, " +
                "(SELECT COUNT(*) FROM pessoas_envolvidas p WHERE id_ocorrencia = o.id) 'PessoasEnvolvidas', " +
                "(SELECT COUNT(*) FROM unidades_moveis u WHERE id_ocorrencia = o.id) 'UnidadesMoveis' FROM ocorrencias o " +
                "LEFT JOIN pessoas_envolvidas p ON o.id = p.id_ocorrencia" +
                "LEFT JOIN unidades_moveis u on o.id = u.id_ocorrencia" +
                "WHERE 42 = 42");

            if (!string.IsNullOrWhiteSpace(query.IdentificadorOcorrencia))
                sql.Append(" AND LOWER(identificador_ocorrencia) LIKE @IdentificadorOcorrencia");

            if (query.Natureza.HasValue)
                sql.Append(" AND LOWER(natureza) = @Natureza");

            if (query.Tipo.HasValue)
                sql.Append(" AND LOWER(tipo) = @Tipo");

            if (!string.IsNullOrWhiteSpace(query.DelegaciaPoliciaApuracao))
                sql.Append(" AND LOWER(delegacia_policia_apuracao) LIKE @DelegaciaPoliciaApuracao");

            if (query.PraticadoPorMenor.HasValue)
                sql.Append(" AND praticado_por_menor = @PraticadoPorMenor");

            if (query.LocalPericiado.HasValue)
                sql.Append(" AND local_periciado = @LocalPericiado");

            if (!string.IsNullOrWhiteSpace(query.TipoLocal))
                sql.Append(" AND LOWER(tipo_local) LIKE @TipoLocal");

            if (!string.IsNullOrWhiteSpace(query.ObjetoMeioEmpregado))
                sql.Append(" AND LOWER(objeto_meio_empregado) LIKE @ObjetoMeioEmpregado");

            if (query.CriadoDe.HasValue)
                sql.Append(" AND created > @CriadoDe");

            if (query.CriadoAte.HasValue)
                sql.Append(" AND created < @CriadoAte");

            if (!string.IsNullOrWhiteSpace(query.PessoaEnvolvidaNome))
                sql.Append(" AND LOWER(nome) LIKE @PessoaEnvolvidaNome");

            if (!string.IsNullOrWhiteSpace(query.PessoaEnvolvidaEnvolvimento))
                sql.Append(" AND LOWER(envolvimento) LIKE @PessoaEnvolvidaEnvolvimento");

            if (!string.IsNullOrWhiteSpace(query.PessoaEnvolvidaCpf))
                sql.Append(" AND LOWER(cpf) LIKE @PessoaEnvolvidaCpf");

            if (!string.IsNullOrWhiteSpace(query.PessoaEnvolvidaProfissao))
                sql.Append(" AND LOWER(profissao) LIKE @PessoaEnvolvidaProfissao");

            if (!string.IsNullOrWhiteSpace(query.PessoaEnvolvidaGravidadeLesoes))
                sql.Append(" AND LOWER(gravidade_lesoes) LIKE @PessoaEnvolvidaGravidadeLesoes");

            if (!string.IsNullOrWhiteSpace(query.PessoaEnvolvidaRacaCor))
                sql.Append(" AND LOWER(raca_cor) LIKE @PessoaEnvolvidaRacaCor");

            if (!string.IsNullOrWhiteSpace(query.UnidadeMovelMatriculaResponsavel))
                sql.Append(" AND LOWER(matricula_responsavel) LIKE @UnidadeMovelMatriculaResponsavel");

            if (!string.IsNullOrWhiteSpace(query.UnidadeMovelUnidadeResponsavel))
                sql.Append(" AND LOWER(unidade_responsavel) LIKE @UnidadeMovelUnidadeResponsavel");

            if (!string.IsNullOrWhiteSpace(query.UnidadeMovelPrefixoVtr))
                sql.Append(" AND LOWER(prefixo_vtr) LIKE @UnidadeMovelPrefixoVtr");

            if (!string.IsNullOrWhiteSpace(query.UnidadeMovelResponsavel))
                sql.Append(" AND LOWER(responsavel) LIKE @UnidadeMovelResponsavel");

            if (query.UnidadeMovelOrgao.HasValue)
                sql.Append(" AND LOWER(orgao) = @UnidadeMovelOrgao");

            if (query.PessoaEnvolvidaSexo.HasValue)
                sql.Append($" AND LOWER(sexo) = LOWER(@PessoaEnvolvidaSexo)");

            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QueryAsync<FilterOcorrenciasVm>(sql.ToString(), query);
        }

        public async Task<FetchOcorrenciaByIdVm> FetchOcorrenciaByIdAsync(Guid id)
        {
            string sql =
                // ocorrencia
                "SELECT id, identificador_ocorrencia as IdentificadorOcorrencia, tipo as Tipo, delegacia_policia_apuracao as DelegaciaPoliciaApuracao, " +
                "natureza as Natureza, data_hora_fato as DataHoraFato, data_hora_comunicacao as DataHoraComunicacao, endereco_fato as EnderecoFato, " +
                "praticado_por_menor as PraticadoPorMenor, local_periciado as LocalPericiado, tipo_local as TipoLocal, " +
                "objeto_meio_empregado as ObjetoMeioEmpregado, created as Created, created_by as CreatedBy FROM ocorrencias " +
                "WHERE id = @Id;" +
                // pessoas envolvidas
                "SELECT id, nome, envolvimento, sexo, cpf, profissao, gravidade_lesoes as GravidadeLesoes, raca_cor as RacaCor, " +
                "id_ocorrencia as IdOcorrencia, created, created_by as CreatedBy FROM pessoas_envolvidas WHERE id_ocorrencia = @Id;" +
                // unidades moveis
                "SELECT id, orgao, prefixo_vtr as PrefixoVtr, responsavel, matricula_responsavel as MatriculaResponsavel, " +
                "unidade_responsavel as UnidadeResponsavel, id_ocorrencia as IdOcorrencia, created, created_by as CreatedBy " +
                "FROM unidades_moveis WHERE id_ocorrencia = @Id;";

            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var multi = await connection.QueryMultipleAsync(sql, new {Id = id});
            var ocorrencia = multi.Read<FetchOcorrenciaByIdVm>().FirstOrDefault();
            if (ocorrencia != null)
            {
                ocorrencia.PessoasEnvolvidas = multi.Read<FetchOcorrenciaByIdPessoaEnvolvida>().ToList();
                ocorrencia.UnidadesMoveis = multi.Read<FetchOcorrenciaByIdUnidadeMovel>().ToList();
            }

            return ocorrencia;
        }
    }
}