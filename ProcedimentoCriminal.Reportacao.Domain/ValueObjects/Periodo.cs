using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.ValueObjects
{
    public class Periodo : ValueObject
    {
        public DateTime Inicio { get; }
        public DateTime Fim { get; }

        public Periodo(DateTime inicio, DateTime fim)
        {
            Inicio = inicio;
            Fim = fim;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Inicio;
            yield return Fim;
        }
    }
}