using MediatR;
using prmToolkit.NotificationPattern;
using System;

namespace Iptm.Domain.Commands.Linha.SalvarLinha
{
    public class SalvarLinhaRequest : IRequest<Response>
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Numero { get; set; }
        public string Codigo { get; set; }
        public int QtdeFrotaMinimaObrigatoria { get; set; }
    }
}
