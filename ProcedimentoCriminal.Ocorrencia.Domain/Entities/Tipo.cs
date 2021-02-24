using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Ocorrencia.Domain.Entities
{
    public class Tipo : Entity
    {
        public string Descricao { get; private set; }

        public Tipo(string descricao)
        {
            Descricao = descricao;
        }
    }
}