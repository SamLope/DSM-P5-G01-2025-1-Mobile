using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class Usuario
    {
        public int Id { get; set; } // Id retornado da API
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; } // "Masculino", "Feminino" ou "Outro"
    }
}
