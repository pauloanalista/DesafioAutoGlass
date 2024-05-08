using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Grupo.SalvarGrupo
{
    public class SalvarAgendaHandler : Notifiable, IRequestHandler<SalvarAgendaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryTemplo _repositoryTemplo;
        private readonly IRepositoryGrupo _repositoryGrupo;

        public SalvarAgendaHandler(IMediator mediator, IRepositoryTemplo repositoryTemplo, IRepositoryGrupo repositoryGrupo)
        {
            _mediator = mediator;
            _repositoryTemplo = repositoryTemplo;
            _repositoryGrupo = repositoryGrupo;
        }

        public async Task<Response> Handle(SalvarAgendaRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var templo = _repositoryTemplo.GetBy(x => x.Id == request.IdTemplo);

            if (templo == null)
            {
                AddNotification("Templo", MSG.X0_E_OBRIGATORIO.ToFormat("Templo"));
                return new Response(this);
            }

            Entities.Grupo grupo = null;
            if (request.Id.HasValue)
            {
                grupo = _repositoryGrupo.GetBy(x => x.Id == request.Id);
                grupo.AlterarGrupo(templo, request.Nome);
            }
            else
            {
                grupo = new Entities.Grupo(templo, request.Nome);
            }
            AddNotifications(grupo);

            if (IsInvalid())
            {
                return new Response(this);
            }

            if (request.Id.HasValue)
            {
                _repositoryGrupo.Update(grupo);
            }
            else
            {
                _repositoryGrupo.Add(grupo);
            }

            //Cria objeto de resposta
            var response = new Response(this, grupo);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}
