using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchUfs
{
    public class FetchUfsQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchUfsQueryHandler : IRequestHandler<FetchUfsQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchUfsQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var uf in Enum.GetValues<Uf>())
                result.Add((int) uf, uf.ToString());

            return Task.FromResult(result);
        }
    }
}