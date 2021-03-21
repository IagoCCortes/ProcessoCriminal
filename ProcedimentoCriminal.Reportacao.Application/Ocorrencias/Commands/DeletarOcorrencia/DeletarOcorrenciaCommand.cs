using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.DeletarOcorrencia
{
    public class DeletarOcorrenciaCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    
    public class DeletarOcorrenciaCommandHandler : IRequestHandler<DeletarOcorrenciaCommand>
    {
        private readonly IOcorrenciaRepository _repository;
        
        public DeletarOcorrenciaCommandHandler(IOcorrenciaRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Unit> Handle(DeletarOcorrenciaCommand request, CancellationToken cancellationToken)
        {
            _repository.Delete(request.Id);
            await _repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}