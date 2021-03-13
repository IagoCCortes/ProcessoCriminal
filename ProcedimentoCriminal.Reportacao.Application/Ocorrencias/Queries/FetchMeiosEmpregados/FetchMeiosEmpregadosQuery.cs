using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchMeiosEmpregados
{
    public class FetchMeiosEmpregadosQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchMeiosEmpregadosQueryHandler : IRequestHandler<FetchMeiosEmpregadosQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchMeiosEmpregadosQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var meio in Enum.GetValues<MeioEmpregado>())
                result.Add((int) meio, meio.ToString());

            return Task.FromResult(result);
        }
    }
}