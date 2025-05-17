using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class RespostaUsuario
    {
        public int IdUsuario { get; set; }         
        public int IdPergunta { get; set; }        
        public string RespostaSimNao { get; set; } // "Sim" ou "Não"
        public int ValorResposta { get; set; }     // 1 para "Sim", 0 para "Não"
    }
}
