using System.Threading.Tasks;
using ProcedimentoCriminal.Core.Application.Interfaces;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories
{
    public class OcorrenciaRepository : BaseRepository, IOcorrenciaRepository
    {
        public OcorrenciaRepository(IDapperConnectionFactory connectionFactory, IDomainEventService domainEventService,
            ICurrentUserService currentUserService, IDateTime dateTime)
            : base(connectionFactory, domainEventService, currentUserService, dateTime)
        {
        }

        public async Task<int> InsertOcorrenciaAsync(Ocorrencia ocorrencia)
        {
            Changes.Add((new OcorrenciaDao(ocorrencia), ChangeType.INSERT));

            foreach (var pessoa in ocorrencia.PessoasEnvolvidas)
            {
                Changes.Add((new PessoaEnvolvidaDao(pessoa, ocorrencia.Id), ChangeType.INSERT));

                foreach (var objeto in pessoa.ObjetosEnvolvidos)
                    Changes.Add((new ObjetoEnvolvidoDao(objeto, pessoa.Id), ChangeType.INSERT));

                foreach (var veiculo in pessoa.VeiculosEnvolvidos)
                    Changes.Add((new VeiculoEnvolvidoDao(veiculo, pessoa.Id), ChangeType.INSERT));
            }

            var changes = await SaveChangesAsync();

            return changes;
        }
    }
}