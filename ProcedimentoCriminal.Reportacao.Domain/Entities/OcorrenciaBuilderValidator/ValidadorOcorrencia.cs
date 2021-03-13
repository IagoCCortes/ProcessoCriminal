using System.Collections.Generic;
using System.Linq;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator
{
    internal abstract class ValidadorOcorrencia
    {
        protected readonly Ocorrencia Ocorrencia;
        protected List<string> Errors;

        public ValidadorOcorrencia(Ocorrencia ocorrencia)
        {
            Ocorrencia = ocorrencia;
            Errors = new List<string>();
        }

        public IEnumerable<string> ValidateTemplateMethod()
        {
            if (Ocorrencia.DelegaciaPoliciaApuracao == null)
                Errors.Add("O campo Delegacia de Polícia Apuração não pode ser vazio");

            if (Ocorrencia.PeriodoFato == null)
                Errors.Add("O Período do fato não pode ser vazio");

            if (Ocorrencia.LocalFato == null)
                Errors.Add("O Local do fato não pode ser vazio");

            ValidarMeiosEmpregados();
            
            ValidarDescricaoFato();

            if (!Ocorrencia.PessoasEnvolvidas.Any())
                Errors.Add("A ocorrência deve conter pessoa(s) envolvida(s)");
            else
                ValidarPessoasEnvolvidas();

            return Errors;
        }

        protected abstract void ValidarMeiosEmpregados();
        protected abstract void ValidarDescricaoFato();
        protected abstract void ValidarPessoasEnvolvidas();

        private void ValidarDescricaoFato(List<string> buildErrors)
        {
            if (Ocorrencia.Natureza.IsOneOf(Natureza.ExtravioPerda, Natureza.AcidenteTransitoSemVitimas))
            {
                if (Ocorrencia.DescricaoFato != null)
                    buildErrors.Add(
                        $"A Natureza de ocorrência {Ocorrencia.Natureza.GetEnumDescription()} não adimite Descricao dos fatos");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Ocorrencia.DescricaoFato))
                    buildErrors.Add(
                        $"A Natureza de ocorrência {Ocorrencia.Natureza.GetEnumDescription()} exige Descricao dos fatos");
            }
        }
        
        protected static bool PossuiComunicanteVitima(IEnumerable<PessoaEnvolvida> pessoasEnvolvidas) =>
            pessoasEnvolvidas.Any(p => p.Envolvimento == Envolvimento.ComunicanteVitima);

        protected static bool PossuiComunicanteEVitima(IEnumerable<PessoaEnvolvida> pessoasEnvolvidas) =>
            pessoasEnvolvidas.Any(p => p.Envolvimento == Envolvimento.Comunicante) &&
            pessoasEnvolvidas.Any(p => p.Envolvimento == Envolvimento.Vitima);

        protected static bool PossuiAutor(IEnumerable<PessoaEnvolvida> pessoasEnvolvidas) =>
            pessoasEnvolvidas.Any(p => p.Envolvimento == Envolvimento.Autor);

        protected static string MensagemNaoAdmite(string natureza, string campo) =>
            $"A Natureza de ocorrência {natureza} não admite {campo}";
        
        protected static string MensagemExige(string natureza, string campo) =>
            $"A Natureza de ocorrência {natureza} exige {campo}";
    }
}