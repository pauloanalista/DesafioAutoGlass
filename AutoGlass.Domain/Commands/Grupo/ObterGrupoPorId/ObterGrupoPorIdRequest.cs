using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace Iptm.Domain.Commands.Grupo.ObterGrupoPorId
{
    public class ObterAgendaPorIdRequest : IRequest<Response>
    {
        public ObterAgendaPorIdRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
