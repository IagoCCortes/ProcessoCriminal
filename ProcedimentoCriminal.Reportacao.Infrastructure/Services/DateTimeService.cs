using System;
using ProcedimentoCriminal.Core.Application.Interfaces;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}