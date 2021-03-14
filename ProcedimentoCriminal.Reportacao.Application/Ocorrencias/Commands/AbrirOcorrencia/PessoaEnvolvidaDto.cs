using System;
using System.Collections.Generic;
using AutoMapper;
using ProcedimentoCriminal.Core.Application.Mappings;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using ProcedimentoCriminal.Reportacao.Domain.ValueObjects;

namespace ProcedimentoCriminal.Reportacao.Application.Ocorrencias.Commands.AbrirOcorrencia
{
    public class PessoaEnvolvidaDto
    {
        public int Envolvimento { get; set; }
        public string Nome { get; set; }
        public int? IdentidadeRg { get; set; }
        public string IdentidadeOrgaoEmissor { get; set; }
        public int? IdentidadeUf { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }
        public DateTime? NascimentoData { get; set; }
        public int? NascimentoUf { get; set; }
        public string CPF { get; set; }
        public char? Sexo { get; set; }
        public string Passaporte { get; set; }
        public int? EstadoCivil { get; set; }
        public int? GrauInstrucao { get; set; }
        public string NomeSocial { get; set; }
        public EnderecoDto EnderecoResidencial { get; set; }
        public EnderecoDto EnderecoComercial { get; set; }
        public List<ObjetoEnvolvidoDto> ObjetosEnvolvidos { get; set; }
        public List<VeiculoEnvolvidoDto> VeiculosEnvolvidos { get; set; }
    }
}