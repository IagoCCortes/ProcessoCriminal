using System;
using System.Linq;
using FluentValidation;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class AbrirOcorrenciaCommandValidations : AbstractValidator<AbrirOcorrenciaCommand>
    {
        public AbrirOcorrenciaCommandValidations()
        {
            RuleFor(a => a.Natureza).Must(n => Enum.IsDefined(typeof(Natureza), n)).WithMessage("A natureza da ocorrência não foi declarada");
            RuleFor(a => a.Tipo).Must(t => Enum.IsDefined(typeof(TipoOcorrencia), t)).WithMessage("O tipo da ocorrência não foi declarado");
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