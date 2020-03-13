﻿using LevelLearn.Domain.Validators;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Net;

namespace LevelLearn.Domain.Services
{
    public class ResponseAPI
    {
        protected ResponseAPI()
        {
            Errors = new List<DadoInvalido>();
        }

        public int StatusCode { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }
        public ICollection<DadoInvalido> Errors { get; private set; }
        public int? PageIndex { get; private set; }
        public int? PageSize { get; private set; }
        public int? Total { get; private set; }

        #region Factory
        public static class ResponseAPIFactory
        {
            public static ResponseAPI Ok(object data, string message, int pageIndex = 0, int pageSize = 0, int total = 0)
            {
                return new ResponseAPI()
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

            public static ResponseAPI Created(object data, string message = "Cadastrado com sucesso")
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.Created,
                    Success = true,
                    Data = data
                };
            }

            public static ResponseAPI NoContent(string message = "Editado com sucesso")
            {
                return new ResponseAPI()
                {
                    Message = message,
                    StatusCode = (int)HttpStatusCode.NoContent,
                    Success = true
                };
            }

            public static ResponseAPI BadRequest(string message = "Dados inválidos", ICollection<DadoInvalido> errors = null)
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
