using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Ocorrencia.Domain.Entities
{
    public class Natureza : Entity

    {
        public string Descricao { get; private set; }

        public Natureza(string descricao)
        {
            Descricao = descricao;
        }
    }
}