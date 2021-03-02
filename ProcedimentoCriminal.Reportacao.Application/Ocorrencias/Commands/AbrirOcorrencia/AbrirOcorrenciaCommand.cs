using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Application.Dtos;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class AbrirOcorrenciaCommand : IRequest
    {
        public string IdentificadorOcorrencia { get; set; }
        public Tipo Tipo { get; set; }
        public string DelegaciaPoliciaApuracao { get; set; }
        public Natureza Natureza { get; set; }
        public DateTime DataHoraFato { get; set; }
        public DateTime DataHoraComunicacao { get; set; }
        public Endereco EnderecoFato { get; set; }
        public bool PraticadoPorMenor { get; set; }
        public bool LocalPericiado { get; set; }
        public string TipoLocal { get; set; }
        public string ObjetoMeioEmpregado { get; set; }
        public List<PessoaEnvolvidaDto> PessoasEnvolvidas { get; set; }
        public List<UnidadeMovelDto> UnidadesMoveis { get; set; }
    }

    public class AbrirOcorrenciaCommandHandler : IRequestHandler<AbrirOcorrenciaCommand>
    {
        private readonly IOcorrenciaRepository _repository;
        private readonly IMapper _mapper;

        public AbrirOcorrenciaCommandHandler(IOcorrenciaRepository repository, IMapper _mapper)
        {
            _repository = repository;
            this._mapper = _mapper;
        }
        
        public async Task<Unit> Handle(AbrirOcorrenciaCommand request, CancellationToken cancellationToken)
        {
            var newOcorrencia = _mapper.Map<AbrirOcorrenciaCommand, Ocorrencia>(request);
            await _repository.InsertOcorrenciaAsync(newOcorrencia);
            return Unit.Value;
        }
    }
}