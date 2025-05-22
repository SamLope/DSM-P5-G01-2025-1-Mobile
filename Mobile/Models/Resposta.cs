using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class Resposta
    {
        public int IdUsuario { get; set; }
        public int IdPergunta { get; set; }
        public bool RespostaSimNao { get; set; }
        public double ValorResposta { get; set; }
    }
}
