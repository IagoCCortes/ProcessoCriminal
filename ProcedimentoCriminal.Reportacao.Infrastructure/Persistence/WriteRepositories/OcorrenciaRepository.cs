using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.WriteRepositories
{
    public class OcorrenciaRepository : GenericWriteRepository<Ocorrencia>, IOcorrenciaRepository
    {
        public OcorrenciaRepository(List<(string sql, DatabaseEntity dbEntity, ChangeType changeType)> changes) :
            base(changes)
        {
        }

        public override void Insert(Ocorrencia ocorrencia)
        {
            var newOcorrencia = new OcorrenciaDao(ocorrencia as Ocorrencia);
            Changes.Add((newOcorrencia.BuildInsertStatement(), newOcorrencia, ChangeType.INSERT));

            foreach (var pessoa in ocorrencia.PessoasEnvolvidas)
            {
                var newPessoaEnvolvida = new PessoaEnvolvidaDao(pessoa, ocorrencia.Id);
                Changes.Add((newPessoaEnvolvida.BuildInsertStatement(), newPessoaEnvolvida, ChangeType.INSERT));

                foreach (var objeto in pessoa.ObjetosEnvolvidos)
                {
                    var newObjeto = new ObjetoEnvolvidoDao(objeto, pessoa.Id);
                    Changes.Add((newObjeto.BuildInsertStatement(), newObjeto, ChangeType.INSERT));
                }

                foreach (var veiculo in pessoa.VeiculosEnvolvidos)
                {
                    var newVeiculo = new VeiculoEnvolvidoDao(veiculo, pessoa.Id);
                    Changes.Add((newVeiculo.BuildInsertStatement(), newVeiculo, ChangeType.INSERT));
                }
            }
        }

        public override void Delete(Guid id)
        {
            var deleteOcorrenciaDao = new OcorrenciaDao(id);
            Changes.Add((deleteOcorrenciaDao.BuildDeleteStatement(), deleteOcorrenciaDao, ChangeType.DELETE));
        }
    }
}