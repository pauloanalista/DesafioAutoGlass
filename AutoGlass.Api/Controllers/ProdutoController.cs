
using AutoGlass.Domain.Commands.Produto.ExcluirProduto;
using AutoGlass.Domain.Commands.Produto.ListarProduto;
using AutoGlass.Domain.Commands.Produto.ObterProdutoPorCodigo;
using AutoGlass.Domain.Commands.Produto.SalvarProduto;
using AutoGlass.Domain.Enums.Produto;
using AutoGlass.Infra.Repositories.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutoGlass.Api.Controllers
{
    public class ProdutoController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProdutoController(IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mediator, configuration, httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected ProdutoController()
        {

        }
        
        [AllowAnonymous]
        [HttpGet("api/Produtos/EnumSituacao")]
        public IActionResult EnumSituacao()
        {
            return Ok(ListarEnum<EnumSituacao>());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Produtos/{codigo}")]
        public async Task<IActionResult> ListarProduto([FromRoute] string codigo )
        {
            try
            {
                var request = new ObterProdutoPorCodigoRequest(codigo);
                Response response = await _mediator.Send(request, CancellationToken.None);

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Produtos")]
        public async Task<IActionResult> ListarProduto([FromQuery] ListarProdutoRequest request)
        {
            try
            {
                Response response = await _mediator.Send(request, CancellationToken.None);

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost]
        [Route("api/Produtos")]
        public async Task<IActionResult> SalvarProduto([FromBody] SalvarProdutoRequest request)
        {
            try
            {
                var response = await _mediator.Send(request, CancellationToken.None);

                return await ResponseAsync(response);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/Produtos")]
        public async Task<IActionResult> EditarProduto([FromBody] SalvarProdutoRequest request)
        {
            try
            {
                var response = await _mediator.Send(request, CancellationToken.None);

                return await ResponseAsync(response);
            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpDelete]
        [Route("api/Produtos/{id:Guid}")]
        public async Task<IActionResult> ExcluirProduto(Guid id)
        {
            try
            {
                var request = new ExcluirProdutoRequest(id);
                var result = await _mediator.Send(request, CancellationToken.None);
                return await ResponseAsync(result);

            }
            catch (System.Exception ex)
            {
                //AdiconarLog(ex);

                return NotFound(ex.Message);
            }
        }
    }
}
