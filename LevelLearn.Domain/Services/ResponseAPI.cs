using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LevelLearn.Domain.Services
{
    public class ResponseAPI
    {
        protected ResponseAPI()
        {
            Errors = new List<ValidationFailure>();
        }

        public int StatusCode { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public ICollection<ValidationFailure> Errors { get; private set; }
        public int Skip { get; private set; }
        public int Limit { get; private set; }
        public int Total { get; private set; }

        #region Factory
        public static class ResponseAPIFactory
        {
            public static ResponseAPI Ok(string message, object data, int skip = 0, int limit = 0, int total = 0)
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.OK,
                    Success = true,
                    Data = data,
                    Skip = skip,
                    Limit = limit,
                    Total = total
                };
            }

            public static ResponseAPI Created(string message, object data)
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.Created,
                    Success = true,
                    Data = data
                };
            }

            public static ResponseAPI NoContent(string message)
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Success = true
                };
            }

            public static ResponseAPI BadRequest(string message = "Dados inválidos", ICollection<ValidationFailure> errors = null)
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Errors = errors
                };
            }

            public static ResponseAPI NotFound(string message = "Não encontrado")
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            public static ResponseAPI InternalServerError(string message = "Erro interno do servidor")
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        } 
        #endregion


    }
}
