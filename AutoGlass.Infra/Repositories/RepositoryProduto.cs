
using AutoGlass.Infra.Repositories.Base;
using AutoGlass.Domain.Entities;
using AutoGlass.Domain.Interfaces.Repositories;
using Ilovecode.EFCore.RepositoryBase;

namespace AutoGlass.Infra.Repositories
{
    public class RepositoryProduto : RepositoryBase<Produto>, IRepositoryProduto
    {
        private readonly Context _context;
        public RepositoryProduto(Context context) : base(context)
        {
            _context = context;
        }
    }
}
