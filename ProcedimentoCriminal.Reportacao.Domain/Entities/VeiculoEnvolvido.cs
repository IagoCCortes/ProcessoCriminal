using System;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Reportacao.Domain.Entities
{
    public class VeiculoEnvolvido : Entity
    {
        public string Marca { get; private set; }
        public string Modelo { get; private set; }
        public TipoVeiculo Tipo { get; private set; }
        public string Cor { get; private set; }
        public CategoriaVeiculo? Categoria { get; private set; }
        public int? AnoFabricacao { get; private set; }
        public int? AnoModelo { get; private set; }
        public Placa Placa { get; set; }
        public Uf? Uf { get; private set; }
        public string Renavam { get; private set; }
        public string Chassi { get; private set; }
        public bool? Segurado { get; private set; }

        public VeiculoEnvolvido(string marca, string modelo, TipoVeiculo tipo, string cor, CategoriaVeiculo categoria,
            int anoFabricacao, int anoModelo, Placa placa, Uf uf, string renavam, string chassi, bool segurado)
        {
            Marca = marca;
            Modelo = modelo;
            Tipo = tipo;
            Cor = cor;
            Categoria = categoria;
            AnoFabricacao = anoFabricacao;
            AnoModelo = anoModelo;
            Placa = placa;
            Uf = uf;
            Renavam = renavam;
            Chassi = chassi;
            Segurado = segurado;
        }
    }
}