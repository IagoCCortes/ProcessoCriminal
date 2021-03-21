using System.Threading.Tasks;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Domain.Interfaces
{
    public interface IOcorrenciaRepository : IGenericWriteRepository<Ocorrencia>
    {
        new void Insert(Ocorrencia ocorrencia);
    }
}