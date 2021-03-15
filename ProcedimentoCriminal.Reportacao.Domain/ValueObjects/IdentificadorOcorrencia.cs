using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.ValueObjects
{
    public class IdentificadorOcorrencia : ValueObject
    {
        public int NmrOcorrencia { get; }
        public int Ano { get; }
        public int NmrDelegacia { get; }

        public IdentificadorOcorrencia(int nmrDelegacia, int ano)
        {
            Ano = ano;
            NmrDelegacia = nmrDelegacia;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NmrOcorrencia;
            yield return Ano;
            yield return NmrDelegacia;
        }

        public override string ToString() => $"/{Ano}-{NmrDelegacia}";
    }
}