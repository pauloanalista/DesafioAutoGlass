using AutoGlass.Domain.Entities;
using AutoGlass.Domain.Interfaces.Repositories;
using MediatR;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace AutoGlass.Tests.Domain.Commands.Produto
{
    public class ProdutoTest
    {

        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IRepositoryProduto> _repositoryProdutoMock;
        private readonly Mock<IRepositoryFornecedor> _repositoryFornecedorMock;

        public ProdutoTest(Mock<IMediator> mediatorMock, Mock<IRepositoryProduto> repositoryProdutoMock, Mock<IRepositoryFornecedor> repositoryFornecedorMock)
        {
            _mediatorMock=mediatorMock;
            _repositoryProdutoMock=repositoryProdutoMock;
            _repositoryFornecedorMock=repositoryFornecedorMock;
        }

        [Fact]
        public void SalvarProduto_Sucesso()
        {
            var fornecedor = _repositoryFornecedorMock.Object.GetAll().FirstOrDefault();
            
            var produto = new AutoGlass.Domain.Entities.Produto(fornecedor, "123", "Apple", DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1));

            
        }
    }
}
