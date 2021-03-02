using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence
{
    public class OcorrenciaRepository : IOcorrenciaRepository
    {
        private readonly DapperConnectionFactory _connectionFactory;
        private readonly IDomainEventService _domainEventService;

        public OcorrenciaRepository(DapperConnectionFactory connectionFactory, IDomainEventService domainEventService)
        {
            _connectionFactory = connectionFactory;
            _domainEventService = domainEventService;
        }

        public async Task InsertOcorrenciaAsync(Ocorrencia ocorrencia)
        {
            if (ocorrencia is IHasDomainEvent && ocorrencia.DomainEvents.Any())
            {
                foreach (var domainEvent in ocorrencia.DomainEvents)
                {
                    _domainEventService.Publish(domainEvent);
                }
            }

            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var transactionScope = new TransactionScope();
            await connection.ExecuteAsync(
                "INSERT INTO ocorrencias "
                + "(id, identificador_ocorrencia, tipo, delegacia_policia_apuracao, natureza, data_hora_fato, data_hora_comunicacao, "
                + "endereco_fato, praticado_por_menor, local_periciado, tipo_local, objeto_meio_empregado) "
                + "VALUES (@id, @identificador_ocorrencia, @tipo, @delegacia_policia_apuracao, @natureza, @data_hora_fato, @data_hora_comunicacao, "
                + "@endereco_fato, @praticado_por_menor, @local_periciado, @tipo_local, @objeto_meio_empregado)", new
                {
                    id = ocorrencia.Id, identificador_ocorrencia = ocorrencia.IdentificadorOcorrencia,
                    tipo = ocorrencia.Tipo.ToString(),
                    delegacia_policia_apuracao = ocorrencia.DelegaciaPoliciaApuracao,
                    natureza = ocorrencia.Natureza.ToString(),
                    data_hora_fato = ocorrencia.DataHoraFato,
                    data_hora_comunicacao = ocorrencia.DataHoraComunicacao,
                    endereco_fato = ocorrencia.EnderecoFato, praticado_por_menor = ocorrencia.PraticadoPorMenor,
                    local_periciado = ocorrencia.LocalPericiado,
                    tipo_local = ocorrencia.TipoLocal, objeto_meio_empregado = ocorrencia.ObjetoMeioEmpregado
                });
            foreach (var pessoa in ocorrencia.PessoasEnvolvidas)
            {
                await connection.ExecuteAsync(
                    "INSERT INTO pessoas_envolvidas (nome, envolvimento, sexo, cpf, profissao, gravidade_lesoes, raca_cor) "
                    + "VALUES (@nome, @envolvimento, @sexo, @cpf, @profissao, @gravidade_lesoes, @raca_cor)",
                    new
                    {
                        nome = pessoa.Nome, envolvimento = pessoa.Envolvimento, sexo = pessoa.Sexo,
                        cpf = pessoa.CPF, profissao = pessoa.Profissao, gravidade_lesoes = pessoa.GravidadeLesoes,
                        raca_cor = pessoa.RacaCor
                    });
            }

            foreach (var unidadeMovel in ocorrencia.UnidadesMoveis)
            {
                await connection.ExecuteAsync(
                    "INSERT INTO UnidadesMoveis (orgao, prefixo_vtr, responsavel, matricula_responsavel, unidade_responsavel) "
                    + "VALUES (@orgao, @prefixo_vtr, @responsavel, @matricula_responsavel, @unidade_responsavel)",
                    new
                    {
                        orgao = unidadeMovel.Orgao.ToString(), prefixo_vtr = unidadeMovel.PrefixoVTR,
                        responsavel = unidadeMovel.Responsavel,
                        matricula_responsavel = unidadeMovel.matriculaResponsavel,
                        unidade_responsavel = unidadeMovel.UnidadeResponsavel
                    });
            }

            transactionScope.Complete();
        }
    }
}