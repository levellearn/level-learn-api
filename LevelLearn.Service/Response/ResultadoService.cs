using LevelLearn.Domain.Validators;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LevelLearn.Service.Response
{
    public class ResultadoService<T> where T : class
    {
        public ResultadoService(string mensagem, int statusCode, bool sucesso = false, T dados = null,
            ICollection<DadoInvalido> erros = null, int? total = null)
        {
            Mensagem = mensagem;
            StatusCode = statusCode;
            Sucesso = sucesso;
            Falhou = !sucesso;
            Dados = dados;
            Erros = erros;
            Total = total;
        }

        [JsonPropertyName("message")]
        public string Mensagem { get; private set; }

        [JsonPropertyName("statusCode")]
        public int StatusCode { get; private set; }

        [JsonPropertyName("success")]
        public bool Sucesso { get; private set; }

        [JsonPropertyName("failure")]
        public bool Falhou { get; private set; }

        [JsonPropertyName("data")]
        public T Dados { get; private set; }

        [JsonPropertyName("total")]
        public int? Total { get; private set; }

        [JsonPropertyName("errors")]
        public ICollection<DadoInvalido> Erros { get; private set; }    

    }

}
