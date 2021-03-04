using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOcorrenciaById;
using ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FilterOcorrencias;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Application.Interfaces.ReadRepositories
{
    public interface IOcorrenciaReadRepository
    {
        Task<IEnumerable<FilterOcorrenciasVm>> FilterOcorrenciasAsync(FilterOcorrenciasQuery query);
        Task<FetchOcorrenciaByIdVm> FetchOcorrenciaByIdAsync(Guid id);
    }
}