using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.ValueObjects
{
    public class Nascimento : ValueObject
    {
        public DateTime Data { get; private set; }
        public Uf Uf { get; private set; }

        public Nascimento(DateTime data, Uf uf)
        {
            Data = data;
            Uf = uf;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Data;
            yield return Uf;
        }
    }
}