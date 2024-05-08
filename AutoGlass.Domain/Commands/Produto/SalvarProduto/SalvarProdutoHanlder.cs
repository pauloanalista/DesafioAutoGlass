using AutoGlass.Domain.Commands.Produto.SalvarProduto;
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

namespace AutoGlass.Domain.Commands.Produto.SalvarProduto
{
        public class SalvarProdutoHandler : Notifiable, IRequestHandler<SalvarProdutoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryProduto _repositoryProduto;
        private readonly IRepositoryFornecedor _repositoryFornecedor;

        public SalvarProdutoHandler(IMediator mediator, IRepositoryProduto repositoryProduto, IRepositoryFornecedor repositoryFornecedor)
        {
            _mediator = mediator;
            _repositoryProduto = repositoryProduto;
            _repositoryFornecedor = repositoryFornecedor;
        }

        public async Task<Response> Handle(SalvarProdutoRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var fornecedor = _repositoryFornecedor.GetBy(x => x.Id == request.IdFornecedor);

            if (fornecedor == null)
            {
                AddNotification("Fornecedor", MSG.X0_E_OBRIGATORIO.ToFormat("Fornecedor"));
                return new Response(this);
            }
            Entities.Produto produto = null;

            if (request.IdProduto.HasValue)
            {
                produto = _repositoryProduto.GetBy(x => x.Id == request.IdProduto);
                produto.AlterarProduto(fornecedor, request.Codigo, request.Descricao, request.Situacao, request.DataFabricacao, request.DataValidade);
            }
            else
            {
                produto = new Entities.Produto(fornecedor, request.Codigo, request.Descricao, request.Situacao, request.DataFabricacao, request.DataValidade);
            }

            AddNotifications(produto);

            if (IsInvalid())
            {
                return new Response(this);
            }

            if (request.IdProduto.HasValue)
            {
                _repositoryProduto.Update(produto);
            }
            else
            {
                _repositoryProduto.Add(produto);
            }


            //Cria objeto de resposta
            var response = new Response(this, produto);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
