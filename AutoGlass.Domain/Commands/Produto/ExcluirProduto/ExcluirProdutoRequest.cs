using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace AutoGlass.Domain.Commands.Produto.ExcluirProduto
{
    public class ExcluirProdutoRequest : IRequest<Response>
    {
        public ExcluirProdutoRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
