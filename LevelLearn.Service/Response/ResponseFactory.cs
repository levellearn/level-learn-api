using LevelLearn.Domain.Validators;
using System.Collections.Generic;
using System.Net;

namespace LevelLearn.Service.Response
{

    public static class ResponseFactory<T> where T : class
    {
        public static ResponseAPI<T> Ok(T data, int? total = null, int? pageNumber = null, int? pageSize = null)
        {
            return new ResponseAPI<T>(
                message: null,
                (int)HttpStatusCode.OK,
                success: true,
                data,
                pageNumber: pageNumber,
                pageSize: pageSize,
                total: total
            );
        }

        public static ResponseAPI<T> Ok(T data)
        {
            return new ResponseAPI<T>(
                message: null,
                (int)HttpStatusCode.OK,
                success: true,
                data
            );
        }

        public static ResponseAPI<T> Ok(T data, string message)
        {
            return new ResponseAPI<T>(
                message,
                (int)HttpStatusCode.OK,
                success: true,
                data
            );
        }

        public static ResponseAPI<T> Created(T data, string message)
        {
            return new ResponseAPI<T>(
                message,
                (int)HttpStatusCode.Created,
                success: true,
                data
            );
        }

        public static ResponseAPI<T> NoContent()
        {
            return new ResponseAPI<T>(null, (int)HttpStatusCode.NoContent, true);
        }

        public static ResponseAPI<T> NoContent(string message)
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.NoContent, true);
        }

        public static ResponseAPI<T> BadRequest(string message)
        {
            return new ResponseAPI<T>(
                message,
                (int)HttpStatusCode.BadRequest,
                errors: new List<DadoInvalido>()
            );
        }

        public static ResponseAPI<T> BadRequest(ICollection<DadoInvalido> dadosInvalidos, string message)
        {
            return new ResponseAPI<T>(
                message,
                (int)HttpStatusCode.BadRequest,
                errors: dadosInvalidos
            );
        }

        public static ResponseAPI<T> BadRequest(DadoInvalido dadoInvalido, string message)
        {
            return new ResponseAPI<T>(
                message,
                (int)HttpStatusCode.BadRequest,
                errors: new List<DadoInvalido>() { dadoInvalido }
            );
        }

        public static ResponseAPI<T> NotFound(string message)
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.NotFound);
        }

        public static ResponseAPI<T> Forbidden(string message)
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.Forbidden);
        }

        public static ResponseAPI<T> InternalServerError(string message)
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.InternalServerError);
        }

    }


}
