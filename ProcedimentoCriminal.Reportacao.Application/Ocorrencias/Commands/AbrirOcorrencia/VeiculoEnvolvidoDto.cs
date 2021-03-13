using AutoMapper;
using ProcedimentoCriminal.Core.Application.Mappings;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class VeiculoEnvolvidoDto : IMapTo<VeiculoEnvolvido>
    {
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public int Tipo { get; private set; }
        public string Cor { get; private set; }
        public int Categoria { get; private set; }
        public int AnoFabricacao { get; private set; }
        public int AnoModelo { get; private set; }
        public string PlacaLetras { get; set; }
        public int PlacaNumero { get; set; }
        public int Uf { get; private set; }
        public string Renavam { get; private set; }
        public string Chassi { get; private set; }
        public bool Segurado { get; private set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VeiculoEnvolvidoDto, VeiculoEnvolvido>().ForMember(v => v.Placa,
                opt => opt.MapFrom(vDto => new Placa(vDto.PlacaLetras, vDto.PlacaNumero)));
        }
    }
}