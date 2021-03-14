using System;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Helper;

namespace ProcedimentoCriminal.Reportacao.Infrastructure.Persistence.Daos
{
    [TableName("veiculos_envolvidos")]
    public class VeiculoEnvolvidoDao : DatabaseEntity
    {
        public string marca { get; private set; }
        public string modelo { get; private set; }
        public int id_tipo { get; private set; }
        public string cor { get; private set; }
        public int? id_categoria { get; private set; }
        public int? ano_fabricacao { get; private set; }
        public int? ano_modelo { get; private set; }
        public string placa { get; set; }
        public int? id_uf { get; private set; }
        public string renavam { get; private set; }
        public string chassi { get; private set; }
        public bool? segurado { get; private set; }
        
        public VeiculoEnvolvidoDao(VeiculoEnvolvido entity, Guid idPessoaEnvolvida) : base(entity)
        {
            marca = entity.Marca;
            modelo = entity.Modelo;
            id_tipo = (int) entity.Tipo;
            cor = entity.Cor;
            id_categoria = (int?) entity.Categoria;
            ano_fabricacao = entity.AnoFabricacao;
            ano_modelo = entity.AnoModelo;
            placa = entity.Placa?.ToString();
            id_uf = (int?) entity.Uf;
            renavam = entity.Renavam;
            chassi = entity.Chassi;
            segurado = entity.Segurado;
        }
    }
}