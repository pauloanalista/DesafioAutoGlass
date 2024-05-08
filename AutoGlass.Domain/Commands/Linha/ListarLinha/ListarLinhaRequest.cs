using MediatR;
using prmToolkit.NotificationPattern;

namespace Iptm.Domain.Commands.Linha.ListarLinha
{
    public class ListarLinhaRequest : IRequest<Response>
    {
        public ListarLinhaRequest()
        {

        }
        public ListarLinhaRequest(string termoPesquisa)
        {
            this.TermoPesquisa = termoPesquisa;
        }

        public string TermoPesquisa { get; set; }
    }
}
