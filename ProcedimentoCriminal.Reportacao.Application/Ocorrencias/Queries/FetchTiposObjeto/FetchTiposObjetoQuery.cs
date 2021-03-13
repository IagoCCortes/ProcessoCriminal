using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchTiposObjeto
{
    public class FetchTiposObjetoQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchTiposObjetoQueryHandler : IRequestHandler<FetchTiposObjetoQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchTiposObjetoQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var tipoObj in Enum.GetValues<TipoObjeto>())
                result.Add((int) tipoObj, tipoObj.ToString());

            return Task.FromResult(result);
        }
    }
}