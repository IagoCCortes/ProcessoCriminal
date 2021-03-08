using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchTipos
{
    public class FetchTiposQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchTiposQueryHandler : IRequestHandler<FetchTiposQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchTiposQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var tipo in Enum.GetValues<TipoOcorrencia>())
                result.Add((int) tipo, tipo.ToString());

            return Task.FromResult(result);
        }
    }
}