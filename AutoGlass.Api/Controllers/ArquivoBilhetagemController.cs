using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Iptm.Domain.Commands.ArquivoBilhetagem.ExcluirArquivoBilhetagem;
using Iptm.Domain.Commands.ArquivoBilhetagem.ListarArquivoBilhetagem;
using Iptm.Domain.Commands.ArquivoBilhetagem.ObterArquivoBilhetagemPorId;
using Iptm.Domain.Commands.ArquivoBilhetagem.SalvarArquivoBilhetagem;
using Iptm.Infra.Repositories.Transactions;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Iptm.Api.Controllers
{
    public class ArquivoBilhetagemController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArquivoBilhetagemController(IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mediator, configuration)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected ArquivoBilhetagemController()
        {

        }

        [Authorize]
        [HttpPost]
        [RequestSizeLimit(2147483648)]//2GB
        [Route("api/ArquivoBilhetagem/Salvar")]
        public async Task<IActionResult> SalvarArquivoBilhetagem([FromForm] SalvarArquivoBilhetagemRequest request)
        {
            try
            {
                var files = Request.Form.Files;

                if (files.Any() == true)
                {
                    var stream = files[0].OpenReadStream();
                    StreamReader reader = new StreamReader(stream);

                    string anexo = reader.ReadToEnd();

                    request.SetAnexo(anexo);
                }

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
        [Route("api/ArquivoBilhetagem/Listar")]
        public async Task<IActionResult> ListarArquivoBilhetagem()
        {
            try
            {
                var request = new ListarArquivoBilhetagemRequest();
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
        [Route("api/ArquivoBilhetagem/ListarPor/{termoPesquisa}")]
        public async Task<IActionResult> ListarArquivoBilhetagem(string termoPesquisa)
        {
            try
            {
                var request = new ListarArquivoBilhetagemRequest(termoPesquisa);
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
        [Route("api/ArquivoBilhetagem/Excluir/{id:Guid}")]
        public async Task<IActionResult> RemoverCliente(Guid id)
        {
            try
            {
                var request = new ExcluirArquivoBilhetagemRequest(id);
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
        [Route("api/ArquivoBilhetagem/ObterPorId/{id:Guid}")]
        public async Task<IActionResult> ObterArquivoBilhetagemPorIdRequest(Guid id)
        {
            try
            {
                var request = new ObterArquivoBilhetagemPorIdRequest(id);
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
