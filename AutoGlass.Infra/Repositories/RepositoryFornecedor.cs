
using AutoGlass.Infra.Repositories.Base;
using AutoGlass.Domain.Entities;
using AutoGlass.Domain.Interfaces.Repositories;
using Ilovecode.EFCore.RepositoryBase;

namespace AutoGlass.Infra.Repositories
{
    public class RepositoryFornecedor : RepositoryBase<Fornecedor>, IRepositoryFornecedor
    {
        private readonly Context _context;
        public RepositoryFornecedor(Context context) : base(context)
        {
            _context = context;
        }
    }
}
