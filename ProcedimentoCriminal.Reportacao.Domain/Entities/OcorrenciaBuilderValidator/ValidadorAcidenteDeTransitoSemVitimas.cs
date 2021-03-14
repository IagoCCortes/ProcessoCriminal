using System.Linq;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator
{
    internal class ValidadorAcidenteDeTransitoSemVitimas : ValidadorOcorrencia
    {
        public ValidadorAcidenteDeTransitoSemVitimas(Ocorrencia ocorrencia) : base(ocorrencia)
        {
        }

        protected override void ValidarMeiosEmpregados()
        {
            if (Ocorrencia.MeiosEmpregados.Any())
                Errors.Add(nameof(Ocorrencia.MeiosEmpregados),
                    new[] {MensagemNaoAdmite(Ocorrencia.Natureza.GetEnumDescription(), "Meios Empregados")});
        }

        protected override void ValidarDescricaoFato()
        {
            if (Ocorrencia.DescricaoFato != null)
                Errors.Add(nameof(Ocorrencia.DescricaoFato),
                    new[] {MensagemNaoAdmite(Ocorrencia.Natureza.GetEnumDescription(), "Descrição dos fatos")});
        }

        protected override void ValidarPessoasEnvolvidas()
        {
            if (!Ocorrencia.PessoasEnvolvidas.Any(p =>
                p.Envolvimento == Envolvimento.Comunicante && p.VeiculosEnvolvidos.Any()))
                Errors.Add(nameof(Ocorrencia.PessoasEnvolvidas),
                    new[]
                    {
                        $"Uma ocorrência de {Ocorrencia.Natureza.GetEnumDescription()} deve conter ao menos um Comunicante e um Veículo"
                    });
        }
    }
}