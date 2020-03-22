using LevelLearn.Domain.Validators;
using System.Collections.Generic;
using System.Net;

namespace LevelLearn.Service.Response
{
    public class ResponseAPI<T> where T : class
    {
        protected ResponseAPI()
        {
            Errors = new List<DadoInvalido>();
        }

        public int StatusCode { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public ICollection<DadoInvalido> Errors { get; private set; }
        public int? PageIndex { get; private set; }
        public int? PageSize { get; private set; }
        public int? Total { get; private set; }

        #region Factory
        public static class ResponseAPIFactory
        {
            public static ResponseAPI<T> Ok(string message)
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true
                };
            }

            public static ResponseAPI<T> Ok(T data, string message, int? pageIndex = null, int? pageSize = null, int? total = null)
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Data = data,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Total = total
                };
            }

            public static ResponseAPI<T> Created(T data, string message = "Cadastrado com sucesso")
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.Created,
                    Success = true,
                    Data = data
                };
            }

            public static ResponseAPI<T> NoContent(string message = "Editado com sucesso")
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Success = true
                };
            }

            public static ResponseAPI<T> BadRequest(string message)
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = new List<DadoInvalido>()
                };
            }

            public static ResponseAPI<T> BadRequest(string message = "Dados inválidos", ICollection<DadoInvalido> dadosInvalidos = null)
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = dadosInvalidos
                };
            }

            public static ResponseAPI<T> BadRequest(string message, DadoInvalido dadoInvalido)
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = new List<DadoInvalido>() { dadoInvalido }
                };
            }

            public static ResponseAPI<T> NotFound(string message = "Não encontrado")
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            public static ResponseAPI<T> InternalServerError(string message = "Erro interno do servidor")
            {
                return new ResponseAPI<T>()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
        #endregion


    }
}
