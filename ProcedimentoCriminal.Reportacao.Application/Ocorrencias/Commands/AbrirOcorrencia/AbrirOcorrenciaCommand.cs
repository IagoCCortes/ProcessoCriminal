using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProcedimentoCriminal.Core.Application.Mappings;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class AbrirOcorrenciaCommand : IRequest
    {
        public string IdentificadorOcorrencia { get; set; }
        public int Tipo { get; set; }
        public string DelegaciaPoliciaApuracao { get; set; }
        public int Natureza { get; set; }
        public DateTime DataHoraFato { get; set; }
        public EnderecoDto EnderecoFato { get; set; }
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
            var endereco = new Endereco(request.EnderecoFato.CEP, request.EnderecoFato.Endereco,
                request.EnderecoFato.NumeroResidencia, request.EnderecoFato.Complemento, request.EnderecoFato.Bairro,
                request.EnderecoFato.Cidade, request.EnderecoFato.Estado);

            var pessoasEnvolvidas = new List<PessoaEnvolvida>();
            foreach (var pessoa in request.PessoasEnvolvidas)
            {
                pessoasEnvolvidas.Add(new PessoaEnvolvida(pessoa.Nome, pessoa.Envolvimento, pessoa.Sexo, pessoa.CPF,
                    pessoa.Profissao, pessoa.GravidadeLesoes, pessoa.RacaCor));
            }

            var unidadesMoveis = new List<UnidadeMovel>();
            foreach (var unidade in request.UnidadesMoveis)
            {
                unidadesMoveis.Add(new UnidadeMovel((Orgao) unidade.Orgao, unidade.PrefixoVtr, unidade.Responsavel,
                    unidade.matriculaResponsavel, unidade.UnidadeResponsavel));
            }

            var ocorrencia = new Ocorrencia(request.IdentificadorOcorrencia, (TipoOcorrencia) request.Tipo,
                request.DelegaciaPoliciaApuracao, (Natureza) request.Natureza, request.DataHoraFato, DateTime.UtcNow, endereco,
                request.PraticadoPorMenor, request.LocalPericiado, request.TipoLocal, request.ObjetoMeioEmpregado,
                pessoasEnvolvidas, unidadesMoveis);
            await _repository.InsertOcorrenciaAsync(ocorrencia);
            return Unit.Value;
        }
    }
}