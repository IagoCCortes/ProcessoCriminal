using System.Linq;
using FluentValidation;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Application.Validations
{
    public class PessoaEnvolvidaValidations : AbstractValidator<PessoaEnvolvida>
    {
        public PessoaEnvolvidaValidations()
        {
            RuleFor(p => p.Envolvimento).NotEmpty().WithMessage("Envolvimento deve ser declarado");
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome deve ser declarado");
            RuleFor(p => p.Sexo).NotEmpty().WithMessage("Sexo deve ser declarado")
                .Must(s => (new[] {'M', 'F'}).Contains(s)).WithMessage("Sexo deve ser 'M' ou 'F'");
        }
    }
}