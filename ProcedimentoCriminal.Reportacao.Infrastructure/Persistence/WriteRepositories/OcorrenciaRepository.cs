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
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;

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

            var ocorrenciaDao = new OcorrenciaDao(ocorrencia);

            operations.Add(async (connection, transaction) => await connection.ExecuteAsync(
                ocorrenciaDao.BuildInsertStatement(), ocorrenciaDao, transaction: transaction));

            foreach (var pessoa in ocorrencia.PessoasEnvolvidas)
            {
                var pessoaDao = new PessoaEnvolvidaDao(pessoa, ocorrencia.Id);
                operations.Add(async (connection, transaction) => await connection.ExecuteAsync(
                    pessoaDao.BuildInsertStatement(), pessoaDao, transaction: transaction));

                foreach (var objeto in pessoa.ObjetosEnvolvidos)
                {
                    var objetoDao = new ObjetoEnvolvidoDao(objeto, pessoa.Id);
                    operations.Add(async (connection, transaction) => await connection.ExecuteAsync(
                        objetoDao.BuildInsertStatement(), objetoDao, transaction: transaction));
                }
                
                foreach (var veiculo in pessoa.VeiculosEnvolvidos)
                {
                    var veiculoDao = new VeiculoEnvolvidoDao(veiculo, pessoa.Id);
                    operations.Add(async (connection, transaction) => await connection.ExecuteAsync(
                        veiculoDao.BuildInsertStatement(), veiculoDao, transaction: transaction));
                }
            }

            var changes = await ExecuteTransactionAsync(operations);

            if (ocorrencia is IHasDomainEvent && ocorrencia.DomainEvents.Any())
                await DispatchEvents(ocorrencia.DomainEvents);

            return changes;
        }
    }
}