using System;
using FluentValidation;
using ProcedimentoCriminal.Reportacao.Application.Validations;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class AbrirOcorrenciaCommandValidations : AbstractValidator<AbrirOcorrenciaCommand>
    {
        public AbrirOcorrenciaCommandValidations()
        {
            RuleFor(a => a.Natureza).Must(n => Enum.IsDefined(typeof(Natureza), n)).WithMessage("A natureza da ocorrência não foi declarada");
            RuleFor(a => a.Tipo).Must(t => Enum.IsDefined(typeof(Tipo), t)).WithMessage("O tipo da ocorrência não foi declarado");
            RuleFor(a => a.IdentificadorOcorrencia).NotEmpty().WithMessage("O identificador da ocorrência não foi declarado");
            RuleFor(a => a.TipoLocal).NotEmpty().WithMessage("O tipo de local da ocorrência não foi declarado");
            RuleFor(a => a.DelegaciaPoliciaApuracao).NotEmpty().WithMessage("A delegacia de apuração da ocorrência não foi declarada");
            RuleFor(a => a.ObjetoMeioEmpregado).NotEmpty()
                .WithMessage("O objeto/meio empregado da ocorrência não foi declarado");
            RuleForEach(a => a.PessoasEnvolvidas).SetValidator(new PessoaEnvolvidaDtoValidations());
            RuleForEach(a => a.UnidadesMoveis).SetValidator(new UnidadeMovelDtoValidations());
            // endereço
        }
    }
}