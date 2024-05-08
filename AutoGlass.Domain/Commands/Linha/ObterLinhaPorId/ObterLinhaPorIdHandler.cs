using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Linha.ObterLinhaPorId
{
    public class ObterLinhaPorIdHandler : Notifiable, IRequestHandler<ObterLinhaPorIdRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryLinha _repositoryLinha;

        public ObterLinhaPorIdHandler(IMediator mediator, IRepositoryLinha repositoryLinha)
        {
            _mediator = mediator;
            _repositoryLinha = repositoryLinha;
        }

        public async Task<Response> Handle(ObterLinhaPorIdRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Linha linha = _repositoryLinha.ObterPor(x=>x.Id==request.Id, Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking);

            if (linha == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Linha"));
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Cria objeto de resposta
            var response = new Response(this, linha);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
