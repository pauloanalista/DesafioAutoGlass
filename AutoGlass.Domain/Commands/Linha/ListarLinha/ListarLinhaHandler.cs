using MediatR;
using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Linha.ListarLinha
{
    public class ListarLinhaHandler : Notifiable, IRequestHandler<ListarLinhaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryLinha _repositoryLinha;

        public ListarLinhaHandler(IMediator mediator, IRepositoryLinha repositoryLinha)
        {
            _mediator = mediator;
            _repositoryLinha = repositoryLinha;
        }

        public async Task<Response> Handle(ListarLinhaRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }
            List<Entities.Linha> linhaCollection;
            if (string.IsNullOrEmpty(request.TermoPesquisa))
            {
                linhaCollection = _repositoryLinha.Listar(QueryTrackingBehavior.NoTracking).ToList();
            }
            else
            {
                linhaCollection = _repositoryLinha.ListarPor(x=>x.Nome.Contains(request.TermoPesquisa) || x.Numero.Contains(request.TermoPesquisa) || x.Codigo.Contains(request.TermoPesquisa), QueryTrackingBehavior.NoTracking).ToList();
            }

            //Cria objeto de resposta
            var response = new Response(this, linhaCollection);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
