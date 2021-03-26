using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ProcedimentoCriminal.Reportacao.Application.Interfaces;
using ProcedimentoCriminal.Reportacao.Domain.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.DeletarOcorrencia
{
    public class DeletarOcorrenciaCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    
    public class DeletarOcorrenciaCommandHandler : IRequestHandler<DeletarOcorrenciaCommand>
    {
        private readonly IUnitOfWork _uow;
        
        public DeletarOcorrenciaCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Unit> Handle(DeletarOcorrenciaCommand request, CancellationToken cancellationToken)
        {
            _uow.OcorrenciaRepository.Delete(request.Id);
            await _uow.SaveChangesAsync();

            return Unit.Value;
        }
    }
}