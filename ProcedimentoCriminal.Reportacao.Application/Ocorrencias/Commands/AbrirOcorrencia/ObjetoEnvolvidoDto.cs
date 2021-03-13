using ProcedimentoCriminal.Core.Application.Mappings;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class ObjetoEnvolvidoDto : IMapTo<ObjetoEnvolvido>
    {
        public int TipoObjeto { get; set; }
        public string Descricao { get; set; }
    }
}