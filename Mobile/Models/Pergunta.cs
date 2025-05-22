using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class Pergunta
    {
        public int Id { get; set; }
        public string TextoPergunta { get; set; }
        public string ColunaDataset { get; set; } // Campo adicional do seu banco
    }
}
