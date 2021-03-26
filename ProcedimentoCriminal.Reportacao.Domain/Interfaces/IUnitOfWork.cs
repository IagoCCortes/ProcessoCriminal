using System.Threading.Tasks;

namespace ProcedimentoCriminal.Reportacao.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IOcorrenciaRepository OcorrenciaRepository { get; }
        Task<int> SaveChangesAsync();
    }
}