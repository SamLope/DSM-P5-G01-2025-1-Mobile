using System;
using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Resultado
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("id_usuario")]
        public int IdUsuario { get; set; }

        [JsonPropertyName("resultado")]
        public string ResultadoTexto { get; set; }

        [JsonPropertyName("criado_em")]
        public DateTime CriadoEm { get; set; }  
    }

}
