using AspNetCore.IQueryable.Extensions;
using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Grupo.ListarGrupo
{
    public class ListarAgendaHandler : Notifiable, IRequestHandler<ListarAgendaRequest, Response>
    {
        private readonly IMediator _mediator;
        //private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryGrupo _repositoryGrupo;

        public ListarAgendaHandler(IRepositoryGrupo repositoryGrupo)
        {
            _repositoryGrupo = repositoryGrupo;
        }

        public async Task<Response> Handle(ListarAgendaRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            var collection = _repositoryGrupo.GetAll().Apply(request);

            if (request.IdTemplo.HasValue)
            {
                collection = collection.Where(x => x.Templo.Id== request.IdTemplo);
            }

            //Cria objeto de resposta
            var response = new Response(this, collection.ToList());

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}