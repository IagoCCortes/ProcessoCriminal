using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Investigacao.Domain.ValueObjects
{
    public class IdentificadorInquerito : ValueObject
    {
        public string NumeroInquerito { get; }

        public IdentificadorInquerito(string numeroInquerito) => NumeroInquerito = numeroInquerito;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return NumeroInquerito;
        }

        public override string ToString() => $"{NumeroInquerito[0..4]}/{NumeroInquerito[4..8]}-{NumeroInquerito[8..]}";
    }
}