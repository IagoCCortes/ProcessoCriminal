using System;
using System.Threading.Tasks;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.Interfaces
{
    public interface IGenericWriteRepository<T> where T : IAggregateRoot
    {
        void Insert(T aggregateRoot);
        void Delete(Guid id);
        Task<int> SaveChangesAsync();
    }
}