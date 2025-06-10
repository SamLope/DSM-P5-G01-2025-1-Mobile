using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Resposta
    {
        [JsonPropertyName("id_usuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("id_pergunta")]
        public int IdPergunta { get; set; }

        [JsonPropertyName("resposta_sim_nao")]
        public bool RespostaSimNao { get; set; }

        [JsonPropertyName("valor_resposta")]
        public double ValorResposta { get; set; }

        [JsonIgnore]
        public string ColunaDataSet { get; set; } = string.Empty;
    }
}
