using System;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos
{
    public class ObjetoEnvolvidoDao : DatabaseEntity
    {
        public int tipo_objeto { get; private set; }
        public string descricao { get; private set; }
        public Guid id_pessoa_envolvida { get; private set; }
        
        public ObjetoEnvolvidoDao(ObjetoEnvolvido entity, Guid idPessoaEnvolvida) : base(entity)
        {
            tipo_objeto = (int) entity.TipoObjeto;
            descricao = entity.Descricao;
            id_pessoa_envolvida = idPessoaEnvolvida;
        }
    }
}