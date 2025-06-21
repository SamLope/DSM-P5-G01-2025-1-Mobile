using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class Usuario
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [JsonPropertyName("sexo")]
        public string Sexo { get; set; }

        [JsonPropertyName("criado_em")]
        public DateTime CriadoEm { get; set; }
    }

}
