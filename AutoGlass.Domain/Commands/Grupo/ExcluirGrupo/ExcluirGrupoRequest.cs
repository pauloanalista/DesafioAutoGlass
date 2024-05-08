using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace Iptm.Domain.Commands.Grupo.ExcluirGrupo
{
    public class ExcluirAgendaRequest : IRequest<Response>
    {
        public ExcluirAgendaRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
