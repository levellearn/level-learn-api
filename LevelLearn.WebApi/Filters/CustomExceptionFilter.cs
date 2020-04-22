using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace LevelLearn.WebApi.Filters
{
    /// <summary>
    /// Filtro Customizado de exceções
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="env">IWebHostEnvironment</param>
        public CustomExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Chamado depois de uma action lançar uma exceção
        /// </summary>
        /// <param name="context">ExceptionContext</param>
        public void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";

            if (!_env.IsDevelopment())
            {
                context.Result = new JsonResult(new
                {
                    message = "Ops, ocorreu um erro no sistema!"
                });
                return;
            }

            var exception = context.Exception;

            context.Result = new JsonResult(new
            {
                message = exception.Message,
                innerException = exception.InnerException?.Message,
                stackTrace = exception.StackTrace,
            });
        }
    }

}
