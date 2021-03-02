using System.Threading.Tasks;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Domain.Interfaces
{
    public interface IOcorrenciaRepository
    {
        Task InsertOcorrenciaAsync(Ocorrencia ocorrencia);
    }
}