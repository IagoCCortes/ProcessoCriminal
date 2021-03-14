using System.Linq;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator
{
    internal class ValidadorApropriacaoIndevida : ValidadorOcorrencia
    {
        public ValidadorApropriacaoIndevida(Ocorrencia ocorrencia) : base(ocorrencia)
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
            if (string.IsNullOrWhiteSpace(Ocorrencia.DescricaoFato))
                Errors.Add(nameof(Ocorrencia.DescricaoFato),
                    new[] {MensagemExige(Ocorrencia.Natureza.GetEnumDescription(), "Descrição dos fatos")});
        }

        protected override void ValidarPessoasEnvolvidas()
        {
            if (!(PossuiComunicanteVitima(Ocorrencia.PessoasEnvolvidas) ||
                  PossuiComunicanteEVitima(Ocorrencia.PessoasEnvolvidas)) ||
                !Ocorrencia.PessoasEnvolvidas.Any(p =>
                    p.Envolvimento.IsOneOf(Envolvimento.Vitima, Envolvimento.ComunicanteVitima) &&
                    p.ObjetosEnvolvidos.Any()))
                Errors.Add(nameof(Ocorrencia.PessoasEnvolvidas),
                    new[]
                    {
                        $"Uma ocorrência de {Ocorrencia.Natureza.GetEnumDescription()} deve " +
                        "conter ao menos um Comunicante/Vítima com um Objeto Envolvido ou um Comunicante e uma Vítima com um Objeto Envolvido"
                    });
        }
    }
}