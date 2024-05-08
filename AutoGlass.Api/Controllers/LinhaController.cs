using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Iptm.Domain.Commands.Linha.ExcluirLinha;
using Iptm.Domain.Commands.Linha.ListarLinha;
using Iptm.Domain.Commands.Linha.ObterLinhaPorId;
using Iptm.Domain.Commands.Linha.SalvarLinha;
using Iptm.Infra.Repositories.Transactions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Api.Controllers
{
    public class LinhaController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LinhaController(IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected LinhaController()
        {

        }

        [Authorize]
        [HttpPost]
        [Route("api/Linha/Salvar")]
        public async Task<IActionResult> SalvarLinha([FromBody] SalvarLinhaRequest request)
        {
            try
            {
                var response = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(response);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Linha/Listar")]
        public async Task<IActionResult> ListarLinha()
        {
            try
            {
                var request = new ListarLinhaRequest();
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Linha/ListarPor/{termoPesquisa}")]
        public async Task<IActionResult> ListarLinha(string termoPesquisa)
        {
            try
            {
                var request = new ListarLinhaRequest(termoPesquisa);
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/Linha/Excluir/{id:Guid}")]
        public async Task<IActionResult> RemoverCliente(Guid id)
        {
            try
            {
                var request = new ExcluirLinhaRequest(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(result);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Linha/ObterPorId/{id:Guid}")]
        public async Task<IActionResult> ObterLinhaPorIdRequest(Guid id)
        {
            try
            {
                var request = new ObterLinhaPorIdRequest(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
