using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            string fullActionName = context.ActionDescriptor.DisplayName;
            IDictionary<string, object> actionArguments = context.ActionArguments;            

            _logger.LogInformation("Action Executing: {@FullActionName} {@ActionArguments} ", fullActionName, actionArguments);
        }

        /// <summary>
        /// Executado depois de uma action ser executada
        /// </summary>
        /// <param name="context">ActionExecutedContext</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

    }

}
