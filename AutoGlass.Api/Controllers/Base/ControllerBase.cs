using AutoGlass.Infra.Repositories.Transactions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using prmToolkit.EnumExtension;
using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoGlass.Api.Controllers.Base
{
    public class ControllerBase : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected ControllerBase()
        {

        }

        public ControllerBase(IUnitOfWork unitOfWork, IMediator mediator, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ResponseAsync(Response response)
        {
            if (response.Notifications.Any())
                return BadRequest(response);

            try
            {
                _unitOfWork.SaveChanges();

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Aqui devo logar o erro
                return BadRequest($"Houve um problema interno com o servidor. Entre em contato com o Administrador do sistema caso o problema persista. Erro interno: {ex.Message}");
            }
        }


        //public async void AdiconarLog(Exception ex)
        //{
        //    Guid idAplicacaoCliente = Guid.Parse(_configuration.GetSection("IdAplicacaoCliente").Value);

        //    AdicionarLogRequest adicionarLogRequest = new AdicionarLogRequest(idAplicacaoCliente, ex.Message, Domain.Enums.EnumTipoLog.Error, false);

        //    var response = await _mediator.Send(adicionarLogRequest, CancellationToken.None);
        //    if (response.Success == true)
        //    {
        //        _unitOfWork.SaveChanges();
        //    }


        //}

        public async Task<IActionResult> ResponseExceptionAsync(Exception ex)
        {
            return BadRequest(new { errors = ex.Message, exception = ex.ToString() });
            //return Request.CreateResponse(HttpStatusCode.InternalServerError, new { errors = ex.Message, exception = ex.ToString() });
        }

        protected override void Dispose(bool disposing)
        {
            //Realiza o dispose no serviço para que possa ser zerada as notificações
            //if (_serviceBase != null)
            //{
            //    _serviceBase.Dispose();
            //}

            base.Dispose(disposing);
        }

        public List<object> ListarEnum<T>() where T : Enum
        {
            var enumVals = new List<object>();

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                string description = string.Empty;

                try
                {
                    description = ((T)item).GetDescription();
                }
                catch { }

                enumVals.Add(new
                {
                    id = (int)item,
                    name = ((T)item).GetName(),
                    description = description
                });
            }

            return enumVals;
        }
    }
}
