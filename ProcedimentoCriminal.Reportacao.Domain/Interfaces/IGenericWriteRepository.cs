using System;
using System.Threading.Tasks;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Core.Domain.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Domain.Interfaces
{
    public interface IGenericWriteRepository<in T> where T : IAggregateRoot
    {
        void Insert(T aggregateRoot);
        void Delete(Guid id);
    }
}