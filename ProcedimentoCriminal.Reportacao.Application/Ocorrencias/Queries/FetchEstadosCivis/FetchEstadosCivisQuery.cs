using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchEstadosCivis
{
    public class FetchEstadosCivisQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchEstadosCivisQueryHandler : IRequestHandler<FetchEstadosCivisQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchEstadosCivisQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var estado in Enum.GetValues<EstadoCivil>())
                result.Add((int) estado, estado.ToString());

            return Task.FromResult(result);
        }
    }
}