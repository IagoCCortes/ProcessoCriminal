using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories
{
    public class OcorrenciaRepository : BaseRepository, IOcorrenciaRepository
    {
        public OcorrenciaRepository(IDapperConnectionFactory connectionFactory, IDomainEventService domainEventService,
            ICurrentUserService currentUserService)
            : base(connectionFactory, domainEventService, currentUserService)
        {
        }

        public async Task<int> InsertOcorrenciaAsync(Ocorrencia ocorrencia)
        {
            if (ocorrencia is IHasDomainEvent && ocorrencia.DomainEvents.Any())
                await DispatchEvents(ocorrencia.DomainEvents);

            var operations = new List<Func<IDbConnection, IDbTransaction, Task<int>>>();

            operations.Add(async (connection, transaction) => await connection.ExecuteAsync(
                "INSERT INTO ocorrencias "
                + "(id, identificador_ocorrencia, tipo, delegacia_policia_apuracao, natureza, data_hora_fato, data_hora_comunicacao, "
                + "endereco_fato, praticado_por_menor, local_periciado, tipo_local, objeto_meio_empregado, created, created_by) "
                + "VALUES (@id, @identificador_ocorrencia, @tipo, @delegacia_policia_apuracao, @natureza, @data_hora_fato, @data_hora_comunicacao, "
                + "@endereco_fato, @praticado_por_menor, @local_periciado, @tipo_local, @objeto_meio_empregado, @created, @created_by)",
                new
                {
                    id = ocorrencia.Id, identificador_ocorrencia = ocorrencia.IdentificadorOcorrencia,
                    tipo = ocorrencia.TipoOcorrencia.ToString(),
                    delegacia_policia_apuracao = ocorrencia.DelegaciaPoliciaApuracao,
                    natureza = ocorrencia.Natureza.ToString(),
                    data_hora_fato = ocorrencia.DataHoraFato,
                    data_hora_comunicacao = ocorrencia.DataHoraComunicacao,
                    endereco_fato = ocorrencia.EnderecoFato.ToString(),
                    praticado_por_menor = ocorrencia.PraticadoPorMenor,
                    local_periciado = ocorrencia.LocalPericiado,
                    tipo_local = ocorrencia.TipoLocal, objeto_meio_empregado = ocorrencia.ObjetoMeioEmpregado,
                    created = DateTime.UtcNow, created_by = CurrentUserService.UserId
                }, transaction: transaction));

            foreach (var pessoa in ocorrencia.PessoasEnvolvidas)
            {
                operations.Add(async (connection, transaction) => await connection.ExecuteAsync(
                    "INSERT INTO pessoas_envolvidas (id, nome, envolvimento, sexo, cpf, profissao, gravidade_lesoes, raca_cor, id_ocorrencia, created, created_by) "
                    + "VALUES (@id, @nome, @envolvimento, @sexo, @cpf, @profissao, @gravidade_lesoes, @raca_cor, @id_ocorrencia, @created, @created_by)",
                    new
                    {
                        id = pessoa.Id, nome = pessoa.Nome, envolvimento = pessoa.Envolvimento, sexo = pessoa.Sexo,
                        cpf = pessoa.CPF, profissao = pessoa.Profissao, gravidade_lesoes = pessoa.GravidadeLesoes,
                        raca_cor = pessoa.RacaCor, id_ocorrencia = ocorrencia.Id, created = DateTime.UtcNow,
                        created_by = CurrentUserService.UserId
                    }, transaction: transaction));
            }

            foreach (var unidadeMovel in ocorrencia.UnidadesMoveis)
            {
                operations.Add(async (connection, transaction) => await connection.ExecuteAsync(
                    "INSERT INTO unidades_moveis (id, orgao, prefixo_vtr, responsavel, matricula_responsavel, unidade_responsavel, id_ocorrencia, created, created_by) "
                    + "VALUES (@id, @orgao, @prefixo_vtr, @responsavel, @matricula_responsavel, @unidade_responsavel, @id_ocorrencia, @created, @created_by)",
                    new
                    {
                        id = unidadeMovel.Id, orgao = unidadeMovel.Orgao.ToString(),
                        prefixo_vtr = unidadeMovel.PrefixoVtr,
                        responsavel = unidadeMovel.Responsavel,
                        matricula_responsavel = unidadeMovel.matriculaResponsavel,
                        unidade_responsavel = unidadeMovel.UnidadeResponsavel,
                        id_ocorrencia = ocorrencia.Id, created = DateTime.UtcNow, created_by = CurrentUserService.UserId
                    }, transaction: transaction));
            }

            var changes = await ExecuteTransactionAsync(operations);
            
            if (ocorrencia is IHasDomainEvent && ocorrencia.DomainEvents.Any())
                await DispatchEvents(ocorrencia.DomainEvents);

            return changes;
        }
    }
}