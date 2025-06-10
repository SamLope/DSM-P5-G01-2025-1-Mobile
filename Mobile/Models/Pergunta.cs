using System.Text.Json.Serialization;

namespace Mobile.Models
{
    public class Pergunta
    {
        public int Id { get; set; }

        [JsonPropertyName("coluna_dataset")]
        public string ColunaDataset { get; set; }

        [JsonPropertyName("texto_pergunta")]
        public string TextoPergunta { get; set; }

        public double? ValorDigitado { get; set; }
    }
}
