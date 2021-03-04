using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOrgaos
{
    public class FetchOrgaosQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchOrgaosQueryHandler : IRequestHandler<FetchOrgaosQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchOrgaosQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var orgao in Enum.GetValues<Orgao>())
                result.Add((int) orgao, orgao.ToString());

            return Task.FromResult(result);
        }
    }
}