using System;

namespace ProcedimentoCriminal.Core.Application.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
