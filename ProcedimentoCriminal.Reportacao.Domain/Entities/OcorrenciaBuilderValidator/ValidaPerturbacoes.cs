using System.Linq;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator
{
    internal class ValidaPerturbacoes : ValidadorOcorrencia
    {
        public ValidaPerturbacoes(Ocorrencia ocorrencia) : base(ocorrencia)
        {
        }

        protected override void ValidarMeiosEmpregados()
        {
            if (Ocorrencia.MeiosEmpregados.Any())
                Errors.Add(MensagemNaoAdmite(Ocorrencia.Natureza.GetEnumDescription(), "Meios Empregados"));
        }

        protected override void ValidarDescricaoFato()
        {
            if (string.IsNullOrWhiteSpace(Ocorrencia.DescricaoFato))
                Errors.Add(MensagemExige(Ocorrencia.Natureza.GetEnumDescription(), "Descrição dos fatos"));
        }

        protected override void ValidarPessoasEnvolvidas()
        {
            if (!(PossuiComunicanteVitima(Ocorrencia.PessoasEnvolvidas) ||
                  PossuiComunicanteEVitima(Ocorrencia.PessoasEnvolvidas)))
                Errors.Add(
                    $"Uma ocorrência de {Ocorrencia.Natureza.GetEnumDescription()} deve conter ao menos um Comunicante/Vítima ou um Comunicante e uma Vítima");
        }
    }
}