using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchTiposVeiculo
{
    public class FetchTiposVeiculoQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchTiposVeiculoQueryHandler : IRequestHandler<FetchTiposVeiculoQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchTiposVeiculoQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var tiposVeiculo in Enum.GetValues<TipoVeiculo>())
                result.Add((int) tiposVeiculo, tiposVeiculo.ToString());

            return Task.FromResult(result);
        }
    }
}