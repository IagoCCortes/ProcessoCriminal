using System;
using System.Linq;
using FluentValidation;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class RegistrarOcorrenciaCommandValidations : AbstractValidator<RegistrarOcorrenciaCommand>
    {
        public RegistrarOcorrenciaCommandValidations()
        {
            RuleFor(a => a.Natureza).Must(n => Enum.IsDefined(typeof(Natureza), n))
                .WithMessage("A natureza da ocorrência não foi declarada");
            RuleFor(a => a.FimFato).GreaterThanOrEqualTo(a => a.InicioFato)
                .WithMessage("A Data/hora do fim do fato deve ser maior ou igual à data de início");
            RuleForEach(a => a.MeiosEmpregados).Must(m => Enum.IsDefined(typeof(MeioEmpregado), m))
                .WithMessage("Os Meios empregados da ocorrência contém valor inválido");
            RuleForEach(a => a.PessoasEnvolvidas).SetValidator(new PessoaEnvolvidaDtoValidations());
        }
    }

    public class PessoaEnvolvidaDtoValidations : AbstractValidator<PessoaEnvolvidaDto>
    {
        public PessoaEnvolvidaDtoValidations()
        {
            RuleFor(p => p.Envolvimento).NotEmpty().WithMessage("Envolvimento deve ser declarado")
                .Must(m => Enum.IsDefined(typeof(Envolvimento), m))
                .WithMessage("O Envolvimento da pessoa envolvida contém valor inválido");
        }
    }
}