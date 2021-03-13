﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.Events;
using ProcedimentoCriminal.Reportacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class Ocorrencia : Entity, IAggregateRoot, IHasDomainEvent
    {
        public IdentificadorOcorrencia IdentificadorOcorrencia { get; private set; }
        public DelegaciaPolicia DelegaciaPoliciaApuracao { get; private set; }
        public Natureza Natureza { get; private set; }
        public Periodo PeriodoFato { get; private set; }
        public DateTime DataHoraComunicacao { get; private set; }
        public Endereco LocalFato { get; private set; }
        public string DescricaoFato { get; private set; }
        private List<MeioEmpregado> _meiosEmpregados;

        public ReadOnlyCollection<MeioEmpregado> MeiosEmpregados
        {
            get => _meiosEmpregados.AsReadOnly();
        }

        private List<PessoaEnvolvida> _pessoasEnvolvidas;

        public ReadOnlyCollection<PessoaEnvolvida> PessoasEnvolvidas
        {
            get => _pessoasEnvolvidas.AsReadOnly();
        }

        private Ocorrencia(Natureza natureza)
        {
            Natureza = natureza;
            DataHoraComunicacao = DateTime.UtcNow;
            _pessoasEnvolvidas = new List<PessoaEnvolvida>();
            DomainEvents = new List<DomainEvent>();
        }

        public List<DomainEvent> DomainEvents { get; }

        public class OcorrenciaBuilder
        {
            private Ocorrencia _ocorrencia;
            private ValidadorOcorrencia _validador;

            public OcorrenciaBuilder(Natureza natureza) => Reset(natureza);

            public OcorrenciaBuilder Reset(Natureza natureza)
            {
                _ocorrencia = new Ocorrencia(natureza);
                _validador = natureza switch
                {
                    Natureza.Ameaca => new ValidadorAmeaca(_ocorrencia),
                    Natureza.ViolenciaContraMulher => new ValidadorViolenciaContraMulher(_ocorrencia),
                    Natureza.Ofensas or Natureza.OfensaRacial => new ValidadorOfensas(_ocorrencia),
                    Natureza.Perturbacoes => new ValidaPerturbacoes(_ocorrencia),
                    Natureza.MausTratosAnimais => new ValidadorMausTratosAnimais(_ocorrencia),
                    Natureza.Furtos or Natureza.EstelionatoFraudesApropriacoes => new ValidadorApropriacaoIndevida(_ocorrencia),
                    Natureza.ExtravioPerda => new ValidadorExtravioPerda(_ocorrencia),
                    Natureza.AcidenteTransitoSemVitimas => new ValidadorAcidenteDeTransitoSemVitimas(_ocorrencia),
                };

                return this;
            }

            public OcorrenciaBuilder DefinirCamposComuns(DelegaciaPolicia delegaciaPoliciaApuracao, Periodo periodoFato,
                Endereco localFato)
            {
                _ocorrencia.DelegaciaPoliciaApuracao = delegaciaPoliciaApuracao;
                _ocorrencia.PeriodoFato = periodoFato;
                _ocorrencia.LocalFato = localFato;

                return this;
            }

            public OcorrenciaBuilder DefinirDescricaoDosFatos(string descricaoFatos)
            {
                _ocorrencia.DescricaoFato = descricaoFatos;
                return this;
            }

            public OcorrenciaBuilder VincularPessoaEnvolvida(PessoaEnvolvida pessoaEnvolvida)
            {
                _ocorrencia._pessoasEnvolvidas.Add(pessoaEnvolvida);
                return this;
            }
            
            public OcorrenciaBuilder VincularMeioEmpregado(MeioEmpregado meioEmpregado)
            {
                _ocorrencia._meiosEmpregados.Add(meioEmpregado);
                return this;
            }

            public Ocorrencia Build()
            {
                var buildErrors = _validador.ValidateTemplateMethod();
                if (buildErrors.Any()) throw new DomainException();

                _ocorrencia.DomainEvents.Add(new OcorrenciaCriadaEvent("Test"));
                return _ocorrencia;
            }
        }
    }
}