using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class UsuarioResponse
    {
        [JsonPropertyName("usuario")]
        public Usuario Usuario { get; set; }
    }
}
