using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Linha.ExcluirLinha
{
    public class ExcluirLinhaHandler : Notifiable, IRequestHandler<ExcluirLinhaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryLinha _repositoryLinha;
        private readonly IRepositoryArquivoBilhetagem _repositoryArquivoBilhetagem;

        public ExcluirLinhaHandler(IMediator mediator, IRepositoryLinha repositoryLinha, IRepositoryArquivoBilhetagem repositoryArquivoBilhetagem)
        {
            _mediator = mediator;
            _repositoryLinha = repositoryLinha;
            _repositoryArquivoBilhetagem = repositoryArquivoBilhetagem;
        }

        public async Task<Response> Handle(ExcluirLinhaRequest request, CancellationToken cancellationToken)
        {
            //Valida se o objeto request esta nulo
            if (request == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Request"));
                return new Response(this);
            }

            Entities.Linha linha = _repositoryLinha.ObterPor(x=>x.Id== request.Id, Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll);

            if (linha == null)
            {
                AddNotification("Request", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Linha"));
                return new Response(this);
            }

            //if (_repositoryArquivoBilhetagem.Existe(x=>x.Linha.Id==request.Id, x=>x.Linha))
            //{
            //    AddNotification("Request", MSG.NAO_E_POSSIVEL_EXCLUIR_UMA_X0_ASSOCIADA_A_UMA_X1.ToFormat("Linha","Arquivo de bilhetagem"));
            //    return new Response(this);
            //}

            //Remover a solicitação
            _repositoryLinha.Remover(linha);

            //Cria objeto de resposta
            var response = new Response(this, linha);

            //Cria e Dispara notificação
            //ExcluirClienteNotification excluirClienteNotification = new ExcluirClienteNotification(solicitacao.Id, solicitacao.Nome);
            //await _mediator.Publish(excluirClienteNotification);

            ////Retorna o resultado
            return await Task.FromResult(response);
        }
    }
}