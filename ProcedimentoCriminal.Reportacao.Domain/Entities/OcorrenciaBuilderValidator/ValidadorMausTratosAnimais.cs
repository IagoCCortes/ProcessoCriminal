using System.Linq;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator
{
    internal class ValidadorMausTratosAnimais : ValidadorOcorrencia
    {
        public ValidadorMausTratosAnimais(Ocorrencia ocorrencia) : base(ocorrencia)
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
            if (!Ocorrencia.PessoasEnvolvidas.Any(p =>
                p.Envolvimento == Envolvimento.Comunicante &&
                p.ObjetosEnvolvidos.Any(o => o.TipoObjeto == TipoObjeto.Animal)))
                Errors.Add(
                    $"Uma ocorrência de {Ocorrencia.Natureza.GetEnumDescription()} deve conter ao menos um Comunicante com um Tipo objeto \"Animal\"");
        }
    }
}