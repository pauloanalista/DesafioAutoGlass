using Iptm.Domain.Commands.Grupo.ObterGrupoPorId;
using Iptm.Domain.Commands.Grupo.ListarGrupo;
using Iptm.Domain.Commands.Grupo.SalvarGrupo;
using Iptm.Domain.Entities;
using Iptm.Infra.Repositories.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Iptm.Domain.Commands.Grupo.ExcluirGrupo;

namespace Iptm.Api.Controllers
{
    public class GrupoController : Base.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GrupoController(IMediator mediator, IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mediator, configuration, httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        protected GrupoController()
        {

        }

        [Authorize]
        [HttpPost]
        [RequestSizeLimit(2147483648)]//2GB
        [Route("api/Grupos")]
        public async Task<IActionResult> SalvarGrupo([FromForm] SalvarAgendaRequest request)
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
        
        [Authorize]
        [HttpGet]
        [Route("api/Grupos")]
        public async Task<IActionResult> ListarGrupo([FromQuery] ListarAgendaRequest request)
        {
            try
            {
                //AutenticarUsuarioResponse usuario = this.ObterUsuarioLogado();
                //request.SetIdUsuario(usuario.Id);

                Response response = await _mediator.Send(request, CancellationToken.None);

                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Grupos/{id:Guid}")]
        public async Task<IActionResult> ObterGrupoPorIdRequest(Guid id)
        {
            try
            {
                var request = new ObterGrupoPorIdRequest(id);
                var response = await _mediator.Send(request, CancellationToken.None);


                return Ok(response);
            }
            catch (System.Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/Grupos/{id:Guid}")]
        public async Task<IActionResult> ExcluirGrupo(Guid id)
        {
            try
            {
                var request = new ExcluirGrupoRequest(id);
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
