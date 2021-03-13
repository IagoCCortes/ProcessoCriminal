﻿using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Reportacao.Domain.ValueObjects
{
    public class Placa : ValueObject
    {
        public string Letras { get; private set; }
        public string Numero { get; private set; }

        public Placa(string letras, int numero)
        {
            Letras = letras.ToUpper();
            Numero = numero.ToString("D4");
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Letras;
            yield return Numero;
        }

        public override string ToString() => $"{Letras}-{Numero}";
    }
}