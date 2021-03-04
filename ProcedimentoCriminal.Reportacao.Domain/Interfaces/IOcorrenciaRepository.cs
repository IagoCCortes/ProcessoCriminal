using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProcedimentoCriminal.Reportacao.Domain.Entities;

namespace ProcedimentoCriminal.Reportacao.Domain.Interfaces
{
    public interface IOcorrenciaRepository
    {
        Task<int> InsertOcorrenciaAsync(Ocorrencia ocorrencia);
    }
}