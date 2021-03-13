using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchEnvolvimentos
{
    public class FetchEnvolvimentosQuery : IRequest<Dictionary<int, string>>
    {
    }

    public class FetchEnvolvimentosQueryHandler : IRequestHandler<FetchEnvolvimentosQuery, Dictionary<int, string>>
    {
        public Task<Dictionary<int, string>> Handle(FetchEnvolvimentosQuery request, CancellationToken cancellationToken)
        {
            var result = new Dictionary<int, string>();
            foreach (var envolvimento in Enum.GetValues<Envolvimento>())
                result.Add((int) envolvimento, envolvimento.ToString());

            return Task.FromResult(result);
        }
    }
}