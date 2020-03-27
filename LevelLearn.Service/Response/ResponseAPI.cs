using LevelLearn.Domain.Validators;
using System.Collections.Generic;
using System.Net;

namespace LevelLearn.Service.Response
{
    public class ResponseAPI<T> where T : class
    {
        public ResponseAPI(string message, int statusCode, bool success = false, T data = null,
            ICollection<DadoInvalido> errors = null, int? pageIndex = null, int? pageSize = null, int? total = null)
        {
            Message = message;
            StatusCode = statusCode;
            Success = success;
            Data = data;
            Errors = errors;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
        }

        protected ResponseAPI()
        {
            Errors = new List<DadoInvalido>();
        }

        public string Message { get; private set; }
        public int StatusCode { get; private set; }
        public bool Success { get; private set; }
        public T Data { get; private set; }
        public ICollection<DadoInvalido> Errors { get; private set; }
        public int? PageIndex { get; private set; }
        public int? PageSize { get; private set; }
        public int? Total { get; private set; }

    }

    #region Factory
    public static class ResponseFactory<T> where T : class
    {
        public static ResponseAPI<T> Ok(T data, string message = "Sucesso", int? total = null, int? pageIndex = null, int? pageSize = null)
        {
            return new ResponseAPI<T>(
                message, (int)HttpStatusCode.OK,
                success: true,
                data,
                pageIndex: pageIndex,
                pageSize: pageSize,
                total: total
            );           
        }

        public static ResponseAPI<T> Created(T data, string message = "Cadastrado com sucesso")
        {
            return new ResponseAPI<T>(
                message, (int)HttpStatusCode.Created,
                success: true,
                data
            );            
        }

        public static ResponseAPI<T> NoContent(string message = "Sucesso")
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.NoContent, true);          
        }

        public static ResponseAPI<T> BadRequest(string message)
        {
            return new ResponseAPI<T>(
                message, (int)HttpStatusCode.BadRequest,
                success: true,
                errors: new List<DadoInvalido>()
            );           
        }

        public static ResponseAPI<T> BadRequest(string message = "Dados inválidos", ICollection<DadoInvalido> dadosInvalidos = null)
        {
            return new ResponseAPI<T>(
                message, (int)HttpStatusCode.BadRequest,
                success: true,
                errors: dadosInvalidos
            );          
        }

        public static ResponseAPI<T> BadRequest(string message, DadoInvalido dadoInvalido)
        {
            return new ResponseAPI<T>(
                message, (int)HttpStatusCode.BadRequest,
                success: true,
                errors: new List<DadoInvalido>() { dadoInvalido }
            );            
        }

        public static ResponseAPI<T> NotFound(string message = "Não encontrado")
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.NotFound);         
        }

        public static ResponseAPI<T> Forbidden(string message)
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.Forbidden);           
        }

        public static ResponseAPI<T> InternalServerError(string message = "Erro interno do servidor")
        {
            return new ResponseAPI<T>(message, (int)HttpStatusCode.InternalServerError);     
        }

    }
    #endregion

}
