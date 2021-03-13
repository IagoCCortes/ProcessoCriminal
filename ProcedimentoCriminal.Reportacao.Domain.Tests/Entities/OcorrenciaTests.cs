using System;
using System.Collections.Generic;
using FluentAssertions;
using ProcedimentoCriminal.Core.Domain;
using ProcedimentoCriminal.Reportacao.Domain.Entities;
using ProcedimentoCriminal.Reportacao.Domain.Enums;
using Xunit;

namespace ProcedimentoCriminal.Reportacao.Domain.Tests.Entities
{
    public class OcorrenciaTests
    {
        private readonly Ocorrencia _ocorrencia;

        public OcorrenciaTests()
        {
            _ocorrencia = new Ocorrencia("test", TipoOcorrencia.Flagrante, "dpTest", Natureza.Criminal, DateTime.Now,
                DateTime.Now,
                new Endereco(123, string.Empty, string.Empty, String.Empty, String.Empty, String.Empty, String.Empty),
                false, false, String.Empty, String.Empty, new List<PessoaEnvolvida>(),
                new List<VeiculoEnvolvido>
                    {new VeiculoEnvolvido(Orgao.PM, String.Empty, String.Empty, String.Empty, String.Empty)});
        }

        // [Fact]
        // public void VincularInquerito_GivenEmptyGuid_ThrowsDomainException()
        // {
        //     // arrange
        //     Action action = () => _ocorrencia.VincularInquerito(Guid.Empty);
        //
        //     // act & asset
        //     action.Should().Throw<DomainException>();
        // }
        //
        // [Fact]
        // public void VincularInquerito_GivenGuid_ShouldSetIdInquerito()
        // {
        //     // arrange
        //     var idInquerito = Guid.NewGuid();
        //
        //     // act
        //     _ocorrencia.VincularInquerito(idInquerito);
        //
        //     // asset
        //     _ocorrencia.IdInquerito.Should().Be(idInquerito);
        // }
    }
}