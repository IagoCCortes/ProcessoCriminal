using System;
using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Investigacao.Domain.ValueObjects
{
    public class Telefone : ValueObject
    {
        public int Ddd { get; }
        public int Numero { get; }

        public Telefone(int ddd, int numero)
        {
            Ddd = ddd;
            Numero = numero;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Ddd;
            yield return Numero;
        }


        public override string ToString()
        {
            const int dividend = 10000; // Get last 4 numbers
            var quotient = Math.DivRem(Numero, dividend, out var rest);

            return $"({Ddd}) {quotient}-{rest}";
        }
    }
}