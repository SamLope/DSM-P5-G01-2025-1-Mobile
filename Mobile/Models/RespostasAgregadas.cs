using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class RespostasAgregadas
    {
        [JsonPropertyName("id_usuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("resposta_01")]
        public string Resposta01 { get; set; }

        [JsonPropertyName("resposta_02")]
        public string Resposta02 { get; set; }

        [JsonPropertyName("resposta_03")]
        public string Resposta03 { get; set; }

        [JsonPropertyName("resposta_04")]
        public string Resposta04 { get; set; }

        [JsonPropertyName("resposta_05")]
        public string Resposta05 { get; set; }

        [JsonPropertyName("resposta_06")]
        public string Resposta06 { get; set; }

        [JsonPropertyName("resposta_07")]
        public string Resposta07 { get; set; }

        [JsonPropertyName("resposta_08")]
        public string Resposta08 { get; set; }

        [JsonPropertyName("resposta_09")]
        public string Resposta09 { get; set; }

        [JsonPropertyName("resposta_10")]
        public string Resposta10 { get; set; }
    }
}
