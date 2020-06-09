using LevelLearn.Domain.Utils.Comum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace LevelLearn.WebApi.Filters
{
    /// <summary>
    /// Filtro Customizado de ações do controller
    /// </summary>
    public class CustomActionFilter : IActionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CustomActionFilter> _logger;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="env">IWebHostEnvironment</param>
        /// <param name="logger">ILogger</param>
        public CustomActionFilter(IWebHostEnvironment env, ILogger<CustomActionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// Executado antes de uma action ser executada
        /// </summary>
        /// <param name="context">ActionExecutingContext</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // CRIAÇÃO DO LOG DO REQUEST
            IDictionary<string, object> actionArguments = context.ActionArguments;
            string requestJson = JsonConvert.SerializeObject(actionArguments);

            _logger.LogInformation("Action Executing: Request: {@Request}", requestJson);
        }

        /// <summary>
        /// Executado depois de uma action ser executada
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!(context.Result is ObjectResult response) || IsSuccessStatusCode(response.StatusCode.Value))
                return;

            // CRIAÇÃO DO LOG DO RESPONSE EM CASO DE AÇÃO MALSUCEDIDA
            string dataInfo = new
            {
                User = context.HttpContext.User.Identity.Name,
                IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                Hostname = context.HttpContext.Request.Host.Host,
                AreaAccessed = context.ActionDescriptor.DisplayName,
                DateTime = DateTime.UtcNow
            }.ToString();

            if (response.StatusCode == (int)HttpStatusCode.NotFound)
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Recurso não encontrado: {@Response} {@Info}", response.Value, dataInfo);

            if (response.StatusCode == (int)HttpStatusCode.Forbidden)
                _logger.LogWarning(LoggingEvents.ForbiddenItem, "Recurso sem permissão de acesso: {@Response} {@Info}", response.Value, dataInfo);

            if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
                _logger.LogError(LoggingEvents.InternalServerError, "Erro interno do servidor: {@Response} {@Info}", response.Value, dataInfo);
        }

        /// <summary>
        /// Verifica se é um status code de sucesso
        /// </summary>
        /// <param name="statusCode">Código do status code</param>
        /// <returns></returns>
        public bool IsSuccessStatusCode(int statusCode)
        {
            return ((int)statusCode >= 200) && ((int)statusCode <= 299);
        }


    }
}