using LevelLearn.Domain.Validators;
using System.Collections.Generic;
using System.Net;

namespace LevelLearn.Service.Response
{
    /// <summary>
    /// Factory para o resultado do service sem dados de retorno - ResponseFactory
    /// </summary>
    public static class ResultadoServiceFactory
    {       
        public static ResultadoService Ok(string message)
        {
            return new ResultadoService(
                message,
                (int)HttpStatusCode.OK,
                sucesso: true
            );
        }

        public static ResultadoService Created(string message)
        {
            return new ResultadoService(
                message,
                (int)HttpStatusCode.Created,
                sucesso: true
            );
        }

        public static ResultadoService NoContent()
        {
            return new ResultadoService(null, (int)HttpStatusCode.NoContent, true);
        }

        public static ResultadoService NoContent(string message)
        {
            return new ResultadoService(message, (int)HttpStatusCode.NoContent, true);
        }

        public static ResultadoService BadRequest(string message)
        {
            return new ResultadoService(
                message,
                (int)HttpStatusCode.BadRequest,
                erros: new List<DadoInvalido>()
            );
        }

        public static ResultadoService BadRequest(ICollection<DadoInvalido> dadosInvalidos, string message)
        {
            return new ResultadoService(
                message,
                (int)HttpStatusCode.BadRequest,
                erros: dadosInvalidos
            );
        }

        public static ResultadoService BadRequest(DadoInvalido dadoInvalido, string message)
        {
            return new ResultadoService(
                message,
                (int)HttpStatusCode.BadRequest,
                erros: new List<DadoInvalido>() { dadoInvalido }
            );
        }

        public static ResultadoService NotFound(string message)
        {
            return new ResultadoService(message, (int)HttpStatusCode.NotFound);
        }

        public static ResultadoService Forbidden(string message)
        {
            return new ResultadoService(message, (int)HttpStatusCode.Forbidden);
        }

        public static ResultadoService InternalServerError(string message)
        {
            return new ResultadoService(message, (int)HttpStatusCode.InternalServerError);
        }

    }

    /// <summary>
    /// Factory para o resultado do service com dados de retorno - ResponseFactory
    /// </summary>
    /// <typeparam name="T">Tipo da classe de retorno dos dados</typeparam>
    public static class ResultadoServiceFactory<T> where T : class
    {
        public static ResultadoService<T> Ok(T data, int? total = null)
        {
            return new ResultadoService<T>(
                mensagem: null,
                (int)HttpStatusCode.OK,
                sucesso: true,
                data,
                total: total
            );
        }

        public static ResultadoService<T> Ok(T data)
        {
            return new ResultadoService<T>(
                mensagem: null,
                (int)HttpStatusCode.OK,
                sucesso: true,
                data
            );
        }

        public static ResultadoService<T> Ok(T data, string message)
        {
            return new ResultadoService<T>(
                message,
                (int)HttpStatusCode.OK,
                sucesso: true,
                data
            );
        }

        public static ResultadoService<T> Created(T data, string message)
        {
            return new ResultadoService<T>(
                message,
                (int)HttpStatusCode.Created,
                sucesso: true,
                data
            );
        }

        public static ResultadoService<T> NoContent()
        {
            return new ResultadoService<T>(null, (int)HttpStatusCode.NoContent, true);
        }

        public static ResultadoService<T> NoContent(string message)
        {
            return new ResultadoService<T>(message, (int)HttpStatusCode.NoContent, true);
        }

        public static ResultadoService<T> BadRequest(string message)
        {
            return new ResultadoService<T>(
                message,
                (int)HttpStatusCode.BadRequest,
                erros: new List<DadoInvalido>()
            );
        }

        public static ResultadoService<T> BadRequest(ICollection<DadoInvalido> dadosInvalidos, string message)
        {
            return new ResultadoService<T>(
                message,
                (int)HttpStatusCode.BadRequest,
                erros: dadosInvalidos
            );
        }

        public static ResultadoService<T> BadRequest(DadoInvalido dadoInvalido, string message)
        {
            return new ResultadoService<T>(
                message,
                (int)HttpStatusCode.BadRequest,
                erros: new List<DadoInvalido>() { dadoInvalido }
            );
        }

        public static ResultadoService<T> NotFound(string message)
        {
            return new ResultadoService<T>(message, (int)HttpStatusCode.NotFound);
        }

        public static ResultadoService<T> Forbidden(string message)
        {
            return new ResultadoService<T>(message, (int)HttpStatusCode.Forbidden);
        }

        public static ResultadoService<T> InternalServerError(string message)
        {
            return new ResultadoService<T>(message, (int)HttpStatusCode.InternalServerError);
        }

    }

}
