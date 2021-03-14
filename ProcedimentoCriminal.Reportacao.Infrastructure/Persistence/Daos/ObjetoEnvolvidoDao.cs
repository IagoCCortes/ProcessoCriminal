using System;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos
{
    [TableName("objetos_envolvidos")]
    public class ObjetoEnvolvidoDao : DatabaseEntity
    {
        public int id_tipo { get; private set; }
        public string descricao { get; private set; }
        public Guid id_pessoa_envolvida { get; private set; }
        
        public ObjetoEnvolvidoDao(ObjetoEnvolvido entity, Guid idPessoaEnvolvida) : base(entity)
        {
            id_tipo = (int) entity.TipoObjeto;
            descricao = entity.Descricao;
            id_pessoa_envolvida = idPessoaEnvolvida;
        }
    }
}