using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.ValueObjects
{
    public class DelegaciaPolicia : ValueObject
    {
        public int Numero { get; }
        public Uf Uf { get; }

        public DelegaciaPolicia(int numero, Uf uf)
        {
            Numero = numero;
            Uf = uf;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Numero;
            yield return Uf;
        }

        public override string ToString() => $"{Numero}ª Delegacia de Polícia ({Uf})";
    }
}