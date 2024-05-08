using AspNetCore.IQueryable.Extensions;
using AutoGlass.Domain.Commands.Produto.ListarProduto;
using AutoGlass.Domain.Interfaces.Repositories;
using AutoGlass.Domain.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using prmToolkit.EnumExtension;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Commands.Produto.ListarProduto
{
    
        public class ListarProdutoHandler : Notifiable, IRequestHandler<ListarProdutoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryProduto _repositoryProduto;

        public ListarProdutoHandler(IMediator mediator, IRepositoryProduto repositoryProduto)
        {
            _mediator = mediator;
            _repositoryProduto = repositoryProduto;
        }

        public async Task<Response> Handle(ListarProdutoRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var collection = _repositoryProduto.GetAll().Include(x => x.Fornecedor).AsQueryable();

            if (string.IsNullOrEmpty(request.Descricao) == false)
            {
                collection = collection.Where(x => x.Descricao.Contains(request.Descricao));
            }

            if (request.Situacao.HasValue)
            {
                collection = collection.Where(x => x.Situacao == request.Situacao);
            }

            collection = collection.Apply(request);

            var prepareCollection = collection.Select(x => new
            {
                Id = x.Id,
                Descricao = x.Descricao,
                DataFabricacao = x.DataFabricacao,
                DataValidade = x.DataValidade,
                SituacaoDescricao = x.Situacao.GetDescription(),
                SituacaoCodigo = (int)x.Situacao,
                Fornecedor = new
                {
                    Id = x.Fornecedor.Id,
                    Nome = x.Fornecedor.Descricao
                }
            });

            //Cria objeto de resposta
            var response = new Response(this, prepareCollection);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
