using System;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class UnidadeMovel : Entity
    {
        public Orgao Orgao { get; private set; }
        public string PrefixoVtr { get; private set; }
        public string Responsavel { get; private set; }
        public string matriculaResponsavel { get; private set; }
        public string UnidadeResponsavel { get; private set; }
        public Guid IdOcorrencia { get; private set; }

        public UnidadeMovel(Orgao orgao, string prefixoVtr, string responsavel, string matriculaResponsavel,
            string unidadeResponsavel) : base()
        {
            Orgao = orgao;
            PrefixoVtr = prefixoVtr;
            Responsavel = responsavel;
            this.matriculaResponsavel = matriculaResponsavel;
            UnidadeResponsavel = unidadeResponsavel;
        }
    }
}