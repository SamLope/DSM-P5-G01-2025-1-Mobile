using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class ResultadoListResponse
    {
        [JsonPropertyName("resultados")]
        public List<Resultado> Resultados { get; set; }
    }
}
