using System.Linq;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities.OcorrenciaBuilderValidator
{
    internal class ValidadorViolenciaContraMulher : ValidadorOcorrencia
    {
        public ValidadorViolenciaContraMulher(Ocorrencia ocorrencia) : base(ocorrencia)
        {
        }
        
        protected override void ValidarMeiosEmpregados()
        {
            if (!Ocorrencia.MeiosEmpregados.Any())
                Errors.Add(nameof(Ocorrencia.MeiosEmpregados), new [] {MensagemExige(Ocorrencia.Natureza.GetEnumDescription(), "ao menos um Meio Empregado")});
        }

        protected override void ValidarDescricaoFato()
        {
            if (string.IsNullOrWhiteSpace(Ocorrencia.DescricaoFato))
                Errors.Add(nameof(Ocorrencia.DescricaoFato), new [] {MensagemExige(Ocorrencia.Natureza.GetEnumDescription(), "Descrição dos fatos")});
        }

        protected override void ValidarPessoasEnvolvidas()
        {
            if (!(PossuiComunicanteVitima(Ocorrencia.PessoasEnvolvidas) ||
                  !PossuiAutor(Ocorrencia.PessoasEnvolvidas)))
                Errors.Add(nameof(Ocorrencia.PessoasEnvolvidas), 
                    new [] {$"Uma ocorrência de {Ocorrencia.Natureza.GetEnumDescription()} deve conter ao menos um Comunicante/Vítima ou um Autor"});
        }
    }
}