using Mobile.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://five-dsm-pi-backend.onrender.com";

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<int> CadastrarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            try
            {
                var jsonRequest = JsonSerializer.Serialize(usuario);
                Console.WriteLine($"Enviando para API: {jsonRequest}");

                var response = await _httpClient.PostAsJsonAsync("/usuarios", usuario);
                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Resposta da API: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erro na API: {response.StatusCode} - {responseContent}");
                }

                var usuarioCriado = JsonSerializer.Deserialize<Usuario>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                App.CurrentUserId = usuarioCriado?.Id ?? throw new Exception("ID não retornado pela API");

                return App.CurrentUserId;
            }
            catch (JsonException ex)
            {
                throw new Exception($"Erro ao processar resposta: {ex.Message}");
            }
        }

        public async Task<List<Pergunta>> GetPerguntasAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<PerguntasResponse>("/perguntas");
                return response?.Perguntas ?? new List<Pergunta>();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Falha na comunicação com a API", ex);
            }
        }

        public async Task EnviarRespostasAgregadas(RespostasAgregadas respostas)
        {
            var json = JsonSerializer.Serialize(respostas);
            Console.WriteLine("Payload enviado: " + json);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/respostas", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao enviar resposta: {response.StatusCode} - {errorContent}");
            }
        }

    }
}
