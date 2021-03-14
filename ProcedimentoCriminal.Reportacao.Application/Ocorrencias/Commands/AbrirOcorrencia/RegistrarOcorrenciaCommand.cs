﻿using System;
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
        public DateTime InicioFato { get; set; }
        public DateTime FimFato { get; set; }
        public EnderecoDto LocalFato { get; set; }
        public string DescricaoFato { get; set; }
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

            foreach (var pessoaDto in request.PessoasEnvolvidas)
            {
                var veiculos = _mapper.Map<List<VeiculoEnvolvido>>(pessoaDto.VeiculosEnvolvidos);
                var objetos = _mapper.Map<List<ObjetoEnvolvido>>(pessoaDto.ObjetosEnvolvidos);
                var endResidencial = _mapper.Map<Endereco>(pessoaDto.EnderecoResidencial);
                var endComercial = _mapper.Map<Endereco>(pessoaDto.EnderecoComercial);
                var identidade =
                    !pessoaDto.IdentidadeRg.HasValue || string.IsNullOrWhiteSpace(pessoaDto.IdentidadeOrgaoEmissor) ||
                    !pessoaDto.IdentidadeUf.HasValue
                        ? null
                        : new Identidade(pessoaDto.IdentidadeRg.Value, pessoaDto.IdentidadeOrgaoEmissor,
                            (Uf) pessoaDto.IdentidadeUf);
                var nascimento =
                    !pessoaDto.NascimentoData.HasValue || !pessoaDto.NascimentoUf.HasValue
                        ? null
                        : new Nascimento(pessoaDto.NascimentoData.Value, (Uf) pessoaDto.NascimentoUf);
                var estadoCivil = pessoaDto.EstadoCivil == null ? null : (EstadoCivil?) pessoaDto.EstadoCivil;
                var grauInstrucao = pessoaDto.GrauInstrucao == null ? null : (GrauInstrucao?) pessoaDto.GrauInstrucao;
                var pessoa = new PessoaEnvolvida((Envolvimento) pessoaDto.Envolvimento, pessoaDto.Nome,
                    identidade, pessoaDto.NomeMae, pessoaDto.NomePai, nascimento, pessoaDto.CPF, pessoaDto.Sexo,
                    pessoaDto.Passaporte, estadoCivil, grauInstrucao, pessoaDto.NomeSocial, endResidencial,
                    endComercial, objetos, veiculos);
                ocorrenciaBuilder.VincularPessoaEnvolvida(pessoa);
            }

            foreach (var meio in request.MeiosEmpregados)
                ocorrenciaBuilder.VincularMeioEmpregado((MeioEmpregado) meio);

            await _repository.InsertOcorrenciaAsync(ocorrenciaBuilder.Build());
            return Unit.Value;
        }
    }
}