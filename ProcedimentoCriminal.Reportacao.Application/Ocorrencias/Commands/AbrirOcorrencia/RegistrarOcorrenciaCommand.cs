using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;
using ProcedimentoCriminal.Reportacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class RegistrarOcorrenciaCommand : IRequest
    {
        public int DelegaciaPoliciaApuracaoNumero { get; set; }
        public int DelegaciaPoliciaApuracaoUf { get; set; }
        public int Natureza { get; set; }
        public DateTime InicioFato { get; private set; }
        public DateTime FimFato { get; private set; }
        public EnderecoDto LocalFato { get; set; }
        public string DescricaoFato { get; private set; }
        public List<int> MeiosEmpregados { get; set; }
        public List<PessoaEnvolvidaDto> PessoasEnvolvidas { get; set; }
    }

    public class RegistrarOcorrenciaCommandHandler : IRequestHandler<RegistrarOcorrenciaCommand>
    {
        private readonly IOcorrenciaRepository _repository;
        private readonly IMapper _mapper;

        public RegistrarOcorrenciaCommandHandler(IOcorrenciaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegistrarOcorrenciaCommand request, CancellationToken cancellationToken)
        {
            var localFato = _mapper.Map<Endereco>(request.LocalFato);

            var ocorrenciaBuilder = new Ocorrencia.OcorrenciaBuilder((Natureza) request.Natureza)
                .DefinirCamposComuns(
                    new DelegaciaPolicia(request.DelegaciaPoliciaApuracaoNumero,
                        (Uf) request.DelegaciaPoliciaApuracaoUf), new Periodo(request.InicioFato, request.FimFato),
                    localFato);

            if (!string.IsNullOrWhiteSpace(request.DescricaoFato))
                ocorrenciaBuilder.DefinirDescricaoDosFatos(request.DescricaoFato);

            foreach (var pessoa in request.PessoasEnvolvidas)
            {
                ocorrenciaBuilder.VincularPessoaEnvolvida(_mapper.Map<PessoaEnvolvida>(pessoa));
            }

            foreach (var meio in request.MeiosEmpregados)
            {
                ocorrenciaBuilder.VincularMeioEmpregado((MeioEmpregado) meio);
            }
            
            await _repository.InsertOcorrenciaAsync(ocorrenciaBuilder.Build());
            return Unit.Value;
        }
    }
}