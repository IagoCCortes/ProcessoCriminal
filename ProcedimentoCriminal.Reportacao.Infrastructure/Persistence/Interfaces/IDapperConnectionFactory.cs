using System.Data;
using System.Threading.Tasks;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Interfaces
{
    public interface IDapperConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}