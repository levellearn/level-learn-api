using LevelLearn.Domain.Validators;
using System.Collections.Generic;

namespace LevelLearn.Service.Response
{
    public class ResponseAPI<T> where T : class
    {
        public ResponseAPI(string message, int statusCode, bool success = false, T data = null,
            ICollection<DadoInvalido> errors = null, int? total = null)
        {
            Message = message;
            StatusCode = statusCode;
            Success = success;
            Failure = !success;
            Data = data;
            Errors = errors;
            Total = total;
        }

        public string Message { get; private set; }
        public int StatusCode { get; private set; }
        public bool Success { get; private set; }
        public bool Failure { get; private set; }
        public T Data { get; private set; }
        public int? Total { get; private set; }
        public ICollection<DadoInvalido> Errors { get; private set; }    

    }

}
