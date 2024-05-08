using MediatR;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using Iptm.Domain.Interfaces.Repositories;
using Iptm.Domain.Resources;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Domain.Commands.Linha.SalvarLinha
{
    public class SalvarLinhaHandler : Notifiable, IRequestHandler<SalvarLinhaRequest, Response>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryLinha _repositoryLinha;

        public SalvarLinhaHandler(IMediator mediator, IRepositoryLinha repositoryLinha)
        {
            _mediator = mediator;
            _repositoryLinha = repositoryLinha;
        }

        public async Task<Response> Handle(SalvarLinhaRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                AddNotification("Resquest", MSG.OBJETO_X0_E_OBRIGATORIO.ToFormat("Usuário"));
                return new Response(this);
            }

            
            Entities.Linha linha;
            if (request.Id.HasValue)
            {
                linha = _repositoryLinha.ObterPor(x => x.Id == request.Id.Value);

                if (linha==null)
                {
                    AddNotification("Linha", MSG.DADOS_NAO_ENCONTRADOS);
                    return new Response(this);
                }

                //Verificar se a linha ja existe
                if (_repositoryLinha.Existe(x => x.Codigo == request.Codigo && x.Id!=request.Id))
                {
                    AddNotification("Linha", MSG.ESTE_X0_JA_EXISTE.ToFormat("Código de linha"));
                    return new Response(this);
                }

                linha.AlterarLinha(request.Numero, request.Codigo, request.Nome, request.QtdeFrotaMinimaObrigatoria);
            }
            else
            {
                //Verificar se a linha ja existe
                if (_repositoryLinha.Existe(x => x.Codigo == request.Codigo))
                {
                    AddNotification("Linha", MSG.ESTE_X0_JA_EXISTE.ToFormat("Código de linha"));
                    return new Response(this);
                }

                linha = new Entities.Linha(request.Numero, request.Codigo, request.Nome, request.QtdeFrotaMinimaObrigatoria);
            }
            
            AddNotifications(linha);
            
            if (IsInvalid())
            {
                return new Response(this);
            }

            if (request.Id.HasValue)
            {
                linha = _repositoryLinha.Editar(linha);
            }
            else
            {
                linha = _repositoryLinha.Adicionar(linha);

            }


            //Criar meu objeto de resposta
            var response = new Response(this, linha);

            //AdicionarUsuarioNotification adicionarUsuarioNotification = new AdicionarUsuarioNotification(usuario);

            //await _mediator.Publish(adicionarUsuarioNotification);

            return await Task.FromResult(response);
        }
    }
}
