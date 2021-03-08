using System.Data;
using System.Threading.Tasks;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence
{
    public interface IDapperConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}