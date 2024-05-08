using AutoGlass.Domain.Commands.Produto.ExcluirProduto;
using AutoGlass.Domain.Enums.Produto;
using AutoGlass.Domain.Interfaces.Repositories;
using AutoGlass.Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Commands.Produto.ExcluirProduto
{
    public class ExcluirProdutoHandler : Notifiable, IRequestHandler<ExcluirProdutoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryProduto _repositoryProduto;

        public ExcluirProdutoHandler(IMediator mediator, IRepositoryProduto repositoryCategoia)
        {
            _mediator = mediator;
            _repositoryProduto = repositoryCategoia;
        }

        public async Task<Response> Handle(ExcluirProdutoRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null )
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var produto = _repositoryProduto.GetBy(x => x.Id == request.Id);

            if (produto == null)
            {
                AddNotification("Request", MSG.DADOS_NAO_ENCONTRADOS);
                return new Response(this);
            }

            produto.InativarProduto();
            
            _repositoryProduto.Update(produto);

            //Cria objeto de resposta
            var response = new Response(this, produto);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}