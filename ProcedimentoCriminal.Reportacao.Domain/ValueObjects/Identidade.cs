using System.Collections.Generic;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;

namespace ProcedimentoCriminal.Reportacao.Domain.ValueObjects
{
    public class Identidade : ValueObject
    {
        public int Rg { get; }
        public string OrgaoEmissor { get; }
        public Uf Uf { get; }

        public Identidade(int rg, string orgaoEmissor, Uf uf)
        {
            Rg = rg;
            OrgaoEmissor = orgaoEmissor;
            Uf = uf;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Rg;
            yield return OrgaoEmissor;
            yield return Uf;
        }
    }
}