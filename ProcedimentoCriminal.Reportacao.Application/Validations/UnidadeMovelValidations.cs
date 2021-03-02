using FluentValidation;
using ProcedimentoCriminal.Reportacao.Application.Dtos;

namespace ProcedimentoCriminal.Reportacao.Application.Validations
{
    public class UnidadeMovelValidations : AbstractValidator<UnidadeMovelDto>
    {
        public UnidadeMovelValidations()
        {
            RuleFor(u => u.matriculaResponsavel).NotEmpty()
                .WithMessage("A matrícula do responsável pela unidade móvel deve ser declarada");
            RuleFor(u => u.Orgao).NotNull().WithMessage("O orgão da unidade móvel deve ser declarado");
            RuleFor(u => u.Responsavel).NotNull()
                .WithMessage("O nome do responsável pela unidade móvel deve ser declarado");
            RuleFor(u => u.PrefixoVTR).NotNull().WithMessage("O prefixo da viatura deve ser declarado");
            RuleFor(u => u.UnidadeResponsavel).NotNull().WithMessage("A unidade responsável deve ser declarada");
        }
    }
}