using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace Iptm.Domain.Commands.Linha.ExcluirLinha
{
    public class ExcluirLinhaRequest : IRequest<Response>
    {
        public ExcluirLinhaRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
