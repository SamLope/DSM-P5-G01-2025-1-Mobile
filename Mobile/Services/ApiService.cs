using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mobile.Models;

namespace Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(string baseUrl)
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        //  Cadastrar usuário
        public async Task<int> CreateUserAsync(Usuario usuario)
        {
            var response = await _http.PostAsJsonAsync("/usuarios", usuario);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<CreateResponse>();
            return json.Id;
        }

        //  Obter perguntas
        public async Task<List<Pergunta>> GetQuestionsAsync()
        {
            var resp = await _http.GetFromJsonAsync<QuestionsResponse>("/perguntas");
            return resp.Perguntas;
        }

        // Enviar respostas
        public async Task SendAnswersAsync(int userId, List<RespostaUsuario> respostas)
        {
            var payload = new
            {
                id_usuario = userId,
                respostas = respostas
            };
            var response = await _http.PostAsJsonAsync("/respostas", payload);
            response.EnsureSuccessStatusCode();
        }

        // Classes internas para desserialização
        private class CreateResponse
        {
            public string Message { get; set; }
            public int Id { get; set; }
        }

        private class QuestionsResponse
        {
            public List<Pergunta> Perguntas { get; set; }
        }
    }
}
