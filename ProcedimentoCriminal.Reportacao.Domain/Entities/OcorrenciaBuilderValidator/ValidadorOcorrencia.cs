using System.Collections.Generic;
using System.Linq;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator
{
    internal abstract class ValidadorOcorrencia
    {
        protected readonly Ocorrencia Ocorrencia;
        protected Dictionary<string, string[]> Errors;

        public ValidadorOcorrencia(Ocorrencia ocorrencia)
        {
            Ocorrencia = ocorrencia;
            Errors = new();
        }

        public Dictionary<string, string[]> ValidateTemplateMethod()
        {
            if (Ocorrencia.DelegaciaPoliciaApuracao == null)
                Errors.Add(nameof(Ocorrencia.DelegaciaPoliciaApuracao),
                    new[] {"O campo Delegacia de Polícia Apuração não pode ser vazio"});

            if (Ocorrencia.PeriodoFato == null)
                Errors.Add(nameof(Ocorrencia.PeriodoFato), new[] {"O Período do fato não pode ser vazio"});

            if (Ocorrencia.LocalFato == null)
                Errors.Add(nameof(Ocorrencia.LocalFato), new[] {"O Local do fato não pode ser vazio"});

            ValidarMeiosEmpregados();

            ValidarDescricaoFato();

            if (!Ocorrencia.PessoasEnvolvidas.Any())
                Errors.Add(nameof(Ocorrencia.PessoasEnvolvidas),
                    new[] {"A ocorrência deve conter pessoa(s) envolvida(s)"});
            else
                ValidarPessoasEnvolvidas();

            return Errors;
        }

        protected abstract void ValidarMeiosEmpregados();
        protected abstract void ValidarDescricaoFato();
        protected abstract void ValidarPessoasEnvolvidas();

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