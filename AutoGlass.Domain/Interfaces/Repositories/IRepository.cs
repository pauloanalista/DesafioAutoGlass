using Ilovecode.EFCore.RepositoryBase;
using AutoGlass.Domain.Entities;

namespace AutoGlass.Domain.Interfaces.Repositories
{
    public interface IRepositoryFornecedor : IRepositoryBase<Fornecedor> { }
    public interface IRepositoryProduto : IRepositoryBase<Produto> { }
}
