using System;
using FluentValidation;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Validations
{
    public class UnidadeMovelDtoValidations : AbstractValidator<UnidadeMovelDto>
    {
        public UnidadeMovelDtoValidations()
        {
            RuleFor(u => u.matriculaResponsavel).NotEmpty()
                .WithMessage("A matrícula do responsável pela unidade móvel deve ser declarada");
            RuleFor(u => u.Orgao).Must(o => Enum.IsDefined(typeof(Orgao), o)).WithMessage("O orgão da unidade móvel deve ser declarado");
            RuleFor(u => u.Responsavel).NotNull()
                .WithMessage("O nome do responsável pela unidade móvel deve ser declarado");
            RuleFor(u => u.PrefixoVtr).NotNull().WithMessage("O prefixo da viatura deve ser declarado");
            RuleFor(u => u.UnidadeResponsavel).NotNull().WithMessage("A unidade responsável deve ser declarada");
        }
    }
}