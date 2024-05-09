using AutoGlass.Domain.Enums.Produto;
using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace AutoGlass.Domain.Commands.Produto.SalvarProduto
{
    public class SalvarProdutoRequest : IRequest<Response>
    {
        public SalvarProdutoRequest()
        {

        }
        public Guid? IdProduto { get; set; }
        public string Codigo { get; set; }

        public string Descricao { get;  set; }
        
        public DateTime DataFabricacao { get;  set; }
        public DateTime DataValidade { get;  set; }
        public Guid IdFornecedor { get; set; }
    }
}
