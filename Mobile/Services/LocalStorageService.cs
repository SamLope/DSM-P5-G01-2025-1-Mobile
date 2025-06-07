using System.Text.Json;
using Mobile.Models;

namespace Mobile.Services;

public class LocalStorageService
{
    private const string RespostasKey = "respostas_temp";

    public async Task SaveRespostasAsync(List<Resposta> respostas)
    {
        try
        {
            var json = JsonSerializer.Serialize(respostas);
            await SecureStorage.SetAsync(RespostasKey, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar respostas: {ex.Message}");
        }
    }

    public async Task<List<Resposta>> LoadRespostasAsync()
    {
        try
        {
            var json = await SecureStorage.GetAsync(RespostasKey);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonSerializer.Deserialize<List<Resposta>>(json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar respostas: {ex.Message}");
        }

        return new List<Resposta>();
    }

    public void ClearRespostas()
    {
        SecureStorage.Remove(RespostasKey);
    }
}