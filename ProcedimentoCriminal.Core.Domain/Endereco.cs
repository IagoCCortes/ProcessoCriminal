using System.Collections.Generic;

namespace ProcedimentoCriminal.Core.Domain
{
    public class Endereco : ValueObject
    {
        public int CEP { get; }
        public string EnderecoDescricao { get; }
        public string NumeroResidencia { get; }
        public string Complemento { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Estado { get; }

        public Endereco(int cep, string endereco, string numeroResidencia, string complemento, string bairro, 
            string cidade, string estado)
        {
            CEP = cep;
            EnderecoDescricao = endereco;
            NumeroResidencia = numeroResidencia;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CEP;
            yield return EnderecoDescricao;
            yield return NumeroResidencia;
            yield return Complemento;
            yield return Bairro;
            yield return Cidade;
            yield return Estado;
        }
    }
}