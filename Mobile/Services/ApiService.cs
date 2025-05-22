using Mobile.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Mobile.Services;

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
            // 1. Serializa o objeto para verificação (debug)
            var jsonRequest = JsonSerializer.Serialize(usuario);
            Console.WriteLine($"Enviando para API: {jsonRequest}");

            // 2. Envia para a API
            var response = await _httpClient.PostAsJsonAsync("/usuarios", usuario);
            var responseContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Resposta da API: {responseContent}"); // Debug

            // 3. Verifica status da resposta
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro na API: {response.StatusCode} - {responseContent}");
            }

            // 4. Desserializa a resposta completa
            var usuarioCriado = JsonSerializer.Deserialize<Usuario>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Para aceitar "criado_em" vs "CriadoEm"
            });

            // 5. Retorna o ID gerado
            return usuarioCriado?.Id ?? throw new Exception("ID não retornado pela API");
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
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<Pergunta>>>("/perguntas");
            return response?.Data ?? new List<Pergunta>();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("Falha na comunicação com a API", ex);
        }
    }

    public async Task CadastrarRespostaAsync(Resposta resposta)
    {
        if (resposta == null)
            throw new ArgumentNullException(nameof(resposta));

        var response = await _httpClient.PostAsJsonAsync("/respostas", resposta);
        response.EnsureSuccessStatusCode();
    }

    public async Task CadastrarResultadoAsync(Resultado resultado)
    {
        if (resultado == null)
            throw new ArgumentNullException(nameof(resultado));

        var response = await _httpClient.PostAsJsonAsync("/resultados", resultado);
        response.EnsureSuccessStatusCode();
    }
}