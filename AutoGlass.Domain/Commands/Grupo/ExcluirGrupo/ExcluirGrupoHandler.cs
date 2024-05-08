using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Grupo.ExcluirGrupo
{
    public class ExcluirAgendaHandler : Notifiable, IRequestHandler<ExcluirAgendaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryGrupo _repositoryGrupo;

        public ExcluirAgendaHandler(IMediator mediator, IRepositoryGrupo repositoryCategoia)
        {
            _mediator = mediator;
            _repositoryGrupo = repositoryCategoia;
        }

        public async Task<Response> Handle(ExcluirAgendaRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            //TODO: Verificar se existe algum grupo relacionado

            var Grupo = _repositoryGrupo.GetBy(x => x.Id == request.Id);

            if (Grupo == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Grupo"));
                return new Response(this);
            }

            _repositoryGrupo.Delete(Grupo);

            //Cria objeto de resposta
            var response = new Response(this, Grupo);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}