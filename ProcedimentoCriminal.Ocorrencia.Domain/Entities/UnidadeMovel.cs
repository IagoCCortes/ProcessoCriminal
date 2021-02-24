using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Ocorrencia.Domain.Entities
{
    public class UnidadeMovel : Entity
    {
        public string Orgao { get; private set; }
        public string PrefixoVTR { get; private set; }
        public string Responsavel { get; private set; }
        public string matriculaResponsavel { get; private set; }
        public string UnidadeResponsavel { get; private set; }

        public UnidadeMovel(string orgao, string prefixoVtr, string responsavel, string matriculaResponsavel, string unidadeResponsavel)
        {
            Orgao = orgao;
            PrefixoVTR = prefixoVtr;
            Responsavel = responsavel;
            this.matriculaResponsavel = matriculaResponsavel;
            UnidadeResponsavel = unidadeResponsavel;
        }
    }
}