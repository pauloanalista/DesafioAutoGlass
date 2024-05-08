using AutoGlass.Domain.Interfaces.Repositories;
using AutoGlass.Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace AutoGlass.Domain.Commands.Produto.ObterProdutoPorCodigo
{
    public class ObterProdutoPorCodigoHandler : Notifiable, IRequestHandler<ObterProdutoPorCodigoRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryProduto _repositoryProduto;

        public ObterProdutoPorCodigoHandler(IMediator mediator, IRepositoryProduto repositoryProduto)
        {
            _mediator = mediator;
            _repositoryProduto = repositoryProduto;
        }

        public async Task<Response> Handle(ObterProdutoPorCodigoRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null || string.IsNullOrEmpty(request.Codigo))
            {
                AddNotification("Request", MSG.X0_NAO_INFORMADO.ToFormat("Código do produto"));
                return new Response(this);
            }

            var produto = _repositoryProduto.GetBy(x=>x.Codigo==request.Codigo);

            if (produto == null)
            {
                AddNotification("Request", MSG.DADOS_NAO_ENCONTRADOS);
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Cria objeto de resposta
            var response = new Response(this, produto);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}