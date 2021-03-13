using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchCategoriasVeiculo
{
    public class FetchCategoriasVeiculoQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchCategoriasVeiculoQueryHandler : IRequestHandler<FetchCategoriasVeiculoQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchCategoriasVeiculoQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var categoria in Enum.GetValues<CategoriaVeiculo>())
                result.Add((int) categoria, categoria.ToString());

            return Task.FromResult(result);
        }
    }
}