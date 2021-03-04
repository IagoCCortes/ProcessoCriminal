using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchNaturezas
{
    public class FetchNaturezasQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchNaturezasQueryHandler : IRequestHandler<FetchNaturezasQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchNaturezasQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var natureza in Enum.GetValues<Natureza>())
                result.Add((int) natureza, natureza.ToString());

            return Task.FromResult(result);
        }
    }
}