using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Grupo.ObterGrupoPorId
{
    public class ObterAgendaPorIdHandler : Notifiable, IRequestHandler<ObterAgendaPorIdRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryGrupo _repositoryGrupo;

        public ObterAgendaPorIdHandler(IMediator mediator, IRepositoryGrupo repositoryCategoia)
        {
            _mediator = mediator;
            _repositoryGrupo = repositoryCategoia;
        }

        public async Task<Response> Handle(ObterAgendaPorIdRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var Grupo = _repositoryGrupo.GetAllBy(x => x.Id == request.Id).FirstOrDefault();

            if (Grupo == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Grupo"));
                return new Response(this);
            }

            if (IsInvalid())
            {
                return new Response(this);
            }

            //Cria objeto de resposta
            var response = new Response(this, Grupo);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}