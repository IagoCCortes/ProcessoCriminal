using System.Collections.Generic;
using System.ComponentModel;
using ProcedimentoCriminal.Core.Domain;

namespace ProcedimentoCriminal.Investigacao.Domain.Entities
{
    public class Inquerito : Entity, IAggregateRoot
    {
        public string IdentificadorInquerito { get; private set; }
        public Investigado Investigado { get; private set; }
        public List<string> IncidenciaPenal { get; private set; }
        public Situacao Situacao { get; private set; }
        public Endereco LocalInfracao { get; private set; }
        public List<IAnexo> Anexos { get; private set; }

        public Inquerito(string identificadorInquerito, Investigado investigado, List<string> incidenciaPenal, Situacao situacao, Endereco localInfracao)
        {
            IdentificadorInquerito = identificadorInquerito;
            Investigado = investigado;
            IncidenciaPenal = incidenciaPenal;
            Situacao = situacao;
            LocalInfracao = localInfracao;
        }

        public void VincularAnexo(IAnexo anexo)
        {
            if (anexo == null) throw new DomainException("Nenhum anexo fornecido");
            
            Anexos.Add(anexo);
        }

        public void SolicitarPericia()
        {
            
        }
    }

    public interface IAnexo
    {
    }

    public class NotaCulpa : IAnexo
    {
        
    }

    public class TermoRestituicao : IAnexo
    {
        
    }

    public class AutoApreensao : IAnexo
    {
        
    }

    public enum Situacao
    {
        [Description("Preso em flagrante")]
        PresoEmFlagrante = 0,
        [Description("Foragido")]
        Foragido = 1,
    }

    public class Investigado
    {
    }
}