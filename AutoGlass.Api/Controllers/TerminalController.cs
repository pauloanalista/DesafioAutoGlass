using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Iptm.Domain.Commands.Terminal.ExcluirTerminal;
using Iptm.Domain.Commands.Terminal.ListarTerminal;
using Iptm.Domain.Commands.Terminal.ObterTerminalPorId;
using Iptm.Domain.Commands.Terminal.SalvarTerminal;
using Iptm.Infra.Repositories.Transactions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Api.Controllers
{
    public class TerminalController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TerminalController(IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected TerminalController()
        {

        }

        [Authorize]
        [HttpPost]
        [Route("api/Terminal/Salvar")]
        public async Task<IActionResult> SalvarTerminal([FromBody] SalvarTerminalRequest request)
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
        [Route("api/Terminal/Listar/{idLinha:Guid}")]
        public async Task<IActionResult> ListarTerminal(Guid idLinha)
        {
            try
            {
                var request = new ListarTerminalRequest(idLinha);
                var result = await _mediator.Send(request, CancellationToken.None);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[Authorize]
        //[HttpGet]
        //[Route("api/Terminal/ListarPor/{termoPesquisa}")]
        //public async Task<IActionResult> ListarTerminal(string termoPesquisa)
        //{
        //    try
        //    {
        //        var request = new ListarTerminalRequest(termoPesquisa);
        //        var result = await _mediator.Send(request, CancellationToken.None);
        //        return Ok(result);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [Authorize]
        [HttpDelete]
        [Route("api/Terminal/Excluir/{id:Guid}")]
        public async Task<IActionResult> ExcluirTerminal(Guid id)
        {
            try
            {
                var request = new ExcluirTerminalRequest(id);
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
        [Route("api/Terminal/ObterPorId/{id:Guid}")]
        public async Task<IActionResult> ObterTerminalPorIdRequest(Guid id)
        {
            try
            {
                var request = new ObterTerminalPorIdRequest(id);
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
