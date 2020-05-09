using LevelLearn.Domain.Entities.Comum;
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
            try
            {
                ObjectResult response = context.Result as ObjectResult;

                if (response.StatusCode == (int)HttpStatusCode.NotFound)
                    _logger.LogWarning(LoggingEvents.GetItemNotFound, "Recurso não encontrado: {@Response}", response.Value);

                if (response.StatusCode == (int)HttpStatusCode.Forbidden)
                    _logger.LogWarning(LoggingEvents.ForbiddenItem, "Recurso sem permissão de acesso: {@Response}", response.Value);

                if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
                    _logger.LogError(LoggingEvents.InternalServerError, "Erro interno do servidor: {@Response}", response.Value);
            }
            catch (Exception exception)
            {
                _logger.LogError(LoggingEvents.InternalServerError, exception, "Erro ao criar log");
            }

        }


    }
}