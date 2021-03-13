using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.ValueObjects
{
    public class IdentificadorOcorrencia : ValueObject
    {
        public int NmrOcorrencia { get; private set; }
        public int Ano { get; private set; }
        public int NmrDelegacia { get; private set; }

        public IdentificadorOcorrencia(int nmrDelegacia)
        {
            Ano = DateTime.UtcNow.Year;
            NmrDelegacia = nmrDelegacia;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NmrOcorrencia;
            yield return Ano;
            yield return NmrDelegacia;
        }

        public override string ToString() => $"{NmrOcorrencia}/{Ano}-{NmrDelegacia}";
    }
}