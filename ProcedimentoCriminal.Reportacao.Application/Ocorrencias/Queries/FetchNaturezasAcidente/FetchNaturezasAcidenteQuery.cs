using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchNaturezasAcidente
{
    public class FetchNaturezasAcidenteQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchNaturezasAcidenteQueryHandler : IRequestHandler<FetchNaturezasAcidenteQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchNaturezasAcidenteQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var natureza in Enum.GetValues<NaturezaAcidente>())
                result.Add((int) natureza, natureza.ToString());

            return Task.FromResult(result);
        }
    }
}