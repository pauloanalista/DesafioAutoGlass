using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Attributes;
using AspNetCore.IQueryable.Extensions.Filter;
using AspNetCore.IQueryable.Extensions.Pagination;
using AspNetCore.IQueryable.Extensions.Sort;
using AutoGlass.Domain.Enums.Produto;
using MediatR;
using prmToolkit.NotificationPattern;

namespace AutoGlass.Domain.Commands.Produto.ListarProduto
{
    public class ListarProdutoRequest : ICustomQueryable, IQueryPaging, IQuerySort, IRequest<Response>
    {
        //private Guid _idFornecedor;
        

        //Campos customizados
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public string Sort { get; set; }

        //Filtrar por nome
        [QueryOperator(Operator = WhereOperator.Contains, HasName = "Descricao")]
        public string Descricao { get; set; }
        public EnumSituacao? Situacao { get; set; }

    }
}
