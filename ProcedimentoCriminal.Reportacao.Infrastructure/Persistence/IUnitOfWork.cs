using System.Threading.Tasks;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
    }
}