using FluentValidation;
using ProcedimentoCriminal.Reportacao.Application.Validations;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class AbrirOcorrenciaCommandValidations : AbstractValidator<AbrirOcorrenciaCommand>
    {
        public AbrirOcorrenciaCommandValidations()
        {
            RuleFor(a => a.Natureza).NotEmpty().WithMessage("A natureza da ocorrência não foi declarada");
            RuleFor(a => a.Tipo).NotEmpty().WithMessage("O tipo da ocorrência não foi declarado");
            RuleFor(a => a.IdentificadorOcorrencia).NotEmpty().WithMessage("O identificador da ocorrência não foi declarado");
            RuleFor(a => a.TipoLocal).NotEmpty().WithMessage("O tipo de local da ocorrência não foi declarado");
            RuleFor(a => a.DelegaciaPoliciaApuracao).NotEmpty().WithMessage("A delegacia de apuração da ocorrência não foi declarada");
            RuleFor(a => a.ObjetoMeioEmpregado).NotEmpty().WithMessage("O objeto/meio empregado da ocorrência não foi declarado");

            new PessoaEnvolvidaValidations();
            new UnidadeMovelValidations();
            // endereço
        }
    }
}