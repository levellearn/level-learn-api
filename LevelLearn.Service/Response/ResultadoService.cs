using LevelLearn.Domain.Validators;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LevelLearn.Service.Response
{
    public class ResultadoService
    {
        #region Ctor
        public ResultadoService(string mensagem, int statusCode, bool sucesso = false,
            ICollection<DadoInvalido> erros = null)
        {
            Mensagem = mensagem;
            StatusCode = statusCode;
            Sucesso = sucesso;
            Falhou = !sucesso;
            Erros = erros;
        } 
        #endregion

        #region Props
        [JsonPropertyName("message")]
        public string Mensagem { get; protected set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; protected set; }

        [JsonPropertyName("success")]
        public bool Sucesso { get; protected set; }

        [JsonPropertyName("failure")]
        public bool Falhou { get; protected set; }

        [JsonPropertyName("errors")]
        public ICollection<DadoInvalido> Erros { get; protected set; } 
        #endregion
    }

    public class ResultadoService<T> : ResultadoService where T : class
    {
        public ResultadoService(string mensagem, int statusCode, bool sucesso = false,
                                T dados = null, ICollection<DadoInvalido> erros = null, int? total = null)
            : base(mensagem, statusCode, sucesso, erros)
        {           
            Dados = dados;
            Total = total;
        }

        [JsonPropertyName("data")]
        public T Dados { get; private set; }

        [JsonPropertyName("total")]
        public int? Total { get; private set; }

    }

}
