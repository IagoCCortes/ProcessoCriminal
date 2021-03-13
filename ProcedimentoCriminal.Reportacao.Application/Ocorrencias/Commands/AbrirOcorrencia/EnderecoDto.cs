using ProcedimentoCriminal.Core.Application.Mappings;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class EnderecoDto : IMapTo<Endereco>
    {
        public int CEP { get; set; }
        public string Endereco { get; set; }
        public string NumeroResidencia { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}