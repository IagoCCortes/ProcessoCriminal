using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchGrausInstrucao
{
    public class FetchGrausInstrucaoQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchGrausInstrucaoQueryHandler : IRequestHandler<FetchGrausInstrucaoQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchGrausInstrucaoQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var grau in Enum.GetValues<GrauInstrucao>())
                result.Add((int) grau, grau.ToString());

            return Task.FromResult(result);
        }
    }
}