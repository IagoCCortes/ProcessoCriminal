using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Application.Interfaces.ReadRepositories;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FilterOcorrencias
{
    public class FilterOcorrenciasQuery : IRequest<IEnumerable<FilterOcorrenciasVm>>
    {
        public string IdentificadorOcorrencia { get; set; }
        public Tipo? Tipo { get; set; }
        public Natureza? Natureza { get; set; }
        public string DelegaciaPoliciaApuracao { get; set; }
        public bool? PraticadoPorMenor { get; set; }
        public bool? LocalPericiado { get; set; }
        public string TipoLocal { get; set; }
        public string ObjetoMeioEmpregado { get; set; }
        public DateTime? CriadoDe { get; set; }
        public DateTime? CriadoAte { get; set; }

        public string PessoaEnvolvidaNome { get; set; }
        public string PessoaEnvolvidaEnvolvimento { get; set; }
        public char? PessoaEnvolvidaSexo { get; set; }
        public string PessoaEnvolvidaCpf { get; set; }
        public string PessoaEnvolvidaProfissao { get; set; }
        public string PessoaEnvolvidaGravidadeLesoes { get; set; }
        public string PessoaEnvolvidaRacaCor { get; set; }

        public string UnidadeMovelMatriculaResponsavel { get; set; }
        public string UnidadeMovelUnidadeResponsavel { get; set; }
        public Orgao? UnidadeMovelOrgao { get; set; }
        public string UnidadeMovelPrefixoVtr { get; set; }
        public string UnidadeMovelResponsavel { get; set; }

        public void PrepareStringsForLikeOperation()
        {
            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.PropertyType == typeof (string))
                {
                    var value = prop.GetValue(this);
                    if (value != null)
                        prop.SetValue(this, $"%{value}%");
                }
            }
        }
    }

    public class FilterOcorrenciasQueryHandler : IRequestHandler<FilterOcorrenciasQuery, IEnumerable<FilterOcorrenciasVm>>
    {
        private readonly IOcorrenciaReadRepository _repository;

        public FilterOcorrenciasQueryHandler(IOcorrenciaReadRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<FilterOcorrenciasVm>> Handle(FilterOcorrenciasQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FilterOcorrenciasAsync(request);
        }
    }
}