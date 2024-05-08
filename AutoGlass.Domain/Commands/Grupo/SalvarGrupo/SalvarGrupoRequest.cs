using MediatR;
using prmToolkit.NotificationPattern;
using System;


namespace Iptm.Domain.Commands.Grupo.SalvarGrupo
{
    public class SalvarAgendaRequest : IRequest<Response>
    {
        public Guid? Id { get; set; }
        public Guid IdTemplo { get; set; }
        public string Nome { get;  set; }
    }
}
