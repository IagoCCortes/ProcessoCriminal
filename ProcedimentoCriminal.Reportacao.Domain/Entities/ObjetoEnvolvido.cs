using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class ObjetoEnvolvido : Entity
    {
        public TipoObjeto TipoObjeto { get; private set; }
        public string Descricao { get; private set; }

        public ObjetoEnvolvido(TipoObjeto tipoObjeto, string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) throw new DomainException();
            
            TipoObjeto = tipoObjeto;
            Descricao = descricao;
        }
    }
}