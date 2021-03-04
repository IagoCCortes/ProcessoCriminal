using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Application.Interfaces.ReadRepositories;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Queries.FetchOcorrenciaById
{
    public class FetchOcorrenciaByIdQuery : IRequest<FetchOcorrenciaByIdVm>
    {
        public Guid Id { get; set; }
    }
    
    public class FetchOcorrenciaByIdQueryHandler : IRequestHandler<FetchOcorrenciaByIdQuery, FetchOcorrenciaByIdVm>
    {
        private readonly IOcorrenciaReadRepository _repository;

        public FetchOcorrenciaByIdQueryHandler(IOcorrenciaReadRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<FetchOcorrenciaByIdVm> Handle(FetchOcorrenciaByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FetchOcorrenciaByIdAsync(request.Id);
        }
    }
}