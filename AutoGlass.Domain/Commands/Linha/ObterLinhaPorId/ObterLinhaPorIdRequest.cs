using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace Iptm.Domain.Commands.Linha.ObterLinhaPorId
{
    public class ObterLinhaPorIdRequest : IRequest<Response>
    {
        public ObterLinhaPorIdRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
