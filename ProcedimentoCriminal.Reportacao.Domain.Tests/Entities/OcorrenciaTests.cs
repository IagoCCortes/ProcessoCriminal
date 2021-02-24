using System;
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
            _ocorrencia = new Ocorrencia("test", Tipo.Flagrante, "dpTest", Natureza.Criminal, DateTime.Now,
                DateTime.Now,
                new Endereco(123, string.Empty, string.Empty, String.Empty, String.Empty, String.Empty, String.Empty),
                false, false, String.Empty, String.Empty);
        }
        
        [Fact]
        public void VincularPessoaEnvolvida_GivenNullPessoa_ThrowsDomainException()
        {
            // arrange
            Action action = () => _ocorrencia.VincularPessoaEnvolvida(null);

            // act & asset
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void VincularPessoaEnvolvida_GivenPessoa_ShouldUpdatePessoasVinculadas()
        {
            // arrange
            var pessoa = new PessoaEnvolvida("test", String.Empty, 'H', 12345678, "tester", String.Empty, String.Empty);

            // act
            _ocorrencia.VincularPessoaEnvolvida(pessoa);

            // asset
            _ocorrencia.PessoasEnvolvidas.Should().HaveCount(1);
        }
        
        [Fact]
        public void VincularInquerito_GivenEmptyGuid_ThrowsDomainException()
        {
            // arrange
            Action action = () => _ocorrencia.VincularInquerito(Guid.Empty);

            // act & asset
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void VincularInquerito_GivenGuid_ShouldSetIdInquerito()
        {
            // arrange
            var idInquerito = Guid.NewGuid();

            // act
            _ocorrencia.VincularInquerito(idInquerito);

            // asset
            _ocorrencia.IdInquerito.Should().Be(idInquerito);
        }
        
        [Fact]
        public void VincularUnidadeMovel_GivenNullUnidadeMovel_ThrowsDomainException()
        {
            // arrange
            Action action = () => _ocorrencia.VincularUnidadeMovel(null);

            // act & asset
            action.Should().Throw<DomainException>();
        }

        [Fact]
        public void VincularUnidadeMovel_GivenUnidadeMovel_ShouldSetUnidadeMovel()
        {
            // arrange
            var unidadeMovel = new UnidadeMovel(Orgao.PMDF, String.Empty, String.Empty, String.Empty, String.Empty);

            // act
            _ocorrencia.VincularUnidadeMovel(unidadeMovel);

            // asset
            _ocorrencia.UnidadeMovel.Should().Be(unidadeMovel);
        }
    }
}