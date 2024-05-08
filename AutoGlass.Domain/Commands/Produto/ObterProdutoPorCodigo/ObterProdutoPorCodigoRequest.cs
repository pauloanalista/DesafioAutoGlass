using MediatR;
using prmToolkit.NotificationPattern;

namespace AutoGlass.Domain.Commands.Produto.ObterProdutoPorCodigo
{
    public class ObterProdutoPorCodigoRequest : IRequest<Response>
    {
        public ObterProdutoPorCodigoRequest(string codigo)
        {
            Codigo = codigo;
        }

        public string Codigo { get; set; }
    }
}
