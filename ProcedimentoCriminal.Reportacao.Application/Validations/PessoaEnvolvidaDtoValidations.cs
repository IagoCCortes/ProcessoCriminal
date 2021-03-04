using System.Linq;
using FluentValidation;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia;

namespace ProcedimentoCriminal.Reportacao.Application.Validations
{
    public class PessoaEnvolvidaDtoValidations : AbstractValidator<PessoaEnvolvidaDto>
    {
        public PessoaEnvolvidaDtoValidations()
        {
            RuleFor(p => p.Envolvimento).NotEmpty().WithMessage("Envolvimento deve ser declarado");
            RuleFor(p => p.Nome).NotEmpty().WithMessage("Nome deve ser declarado");
            RuleFor(p => p.Sexo).NotEmpty().WithMessage("Sexo deve ser declarado")
                .Must(s => (new[] {'M', 'F'}).Contains(s)).WithMessage("Sexo deve ser 'M' ou 'F'");
        }
    }
}