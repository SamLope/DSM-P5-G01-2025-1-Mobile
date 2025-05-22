using Mobile.Models;
using Mobile.Services;
using System.Collections.ObjectModel;

namespace Mobile.Views;

public partial class TestePage : ContentPage
{
    private ObservableCollection<Pergunta> perguntas = new();
    private int perguntaAtual = 0;
    private List<Resposta> respostas = new();
    private int usuarioId;

    public TestePage() // Construtor SIMPLES sem injeção
    {
        InitializeComponent();
        CarregarPerguntas();
    }

    private async void CarregarPerguntas()
    {
        try
        {
            loadingIndicator.IsVisible = true;

            // Acessa o ApiService SEM injeção
            var apiService = Handler?.MauiContext?.Services.GetService<ApiService>();

            if (apiService != null)
            {
                var perguntasApi = await apiService.GetPerguntasAsync();
                perguntas = new ObservableCollection<Pergunta>(perguntasApi);
            }

            if (!perguntas.Any())
            {
                await DisplayAlert("Aviso", "Nenhuma pergunta disponível", "OK");
                CarregarPerguntasTeste();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
            CarregarPerguntasTeste();
        }
        finally
        {
            loadingIndicator.IsVisible = false;
            MostrarPerguntaAtual();
        }
    }

    private void CarregarPerguntasTeste()
    {
        perguntas = new ObservableCollection<Pergunta>
        {
            new Pergunta { Id = 1, TextoPergunta = "Você possui conta bancária?" },
            new Pergunta { Id = 2, TextoPergunta = "Possui cartão de crédito?" }
        };
    }

    private void MostrarPerguntaAtual()
    {
        if (perguntaAtual < perguntas.Count)
            lblPergunta.Text = perguntas[perguntaAtual].TextoPergunta;
        else
            FinalizarTeste(); // Mantive o nome original como no seu código
    }

    private async void FinalizarTeste()
    {
        try
        {
            loadingIndicator.IsVisible = true;
            var apiService = Handler?.MauiContext?.Services.GetService<ApiService>();

            if (apiService != null)
            {
                foreach (var resposta in respostas)
                    await apiService.CadastrarRespostaAsync(resposta);

                await apiService.CadastrarResultadoAsync(new Resultado
                {
                    IdUsuario = usuarioId,
                    Pontuacao = respostas.Count(r => r.RespostaSimNao),
                    Score = respostas.Sum(r => r.ValorResposta)
                });
            }

            if (Parent is TabbedPage tabbedPage)
                tabbedPage.CurrentPage = tabbedPage.Children[3];
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
        finally
        {
            loadingIndicator.IsVisible = false;
        }
    }

    private void OnSimClicked(object sender, EventArgs e)
    {
        RegistrarResposta(true);
    }

    private void OnNaoClicked(object sender, EventArgs e)
    {
        RegistrarResposta(false);
    }

    private void RegistrarResposta(bool resposta)
    {
        if (perguntaAtual < perguntas.Count)
        {
            respostas.Add(new Resposta
            {
                IdUsuario = usuarioId,
                IdPergunta = perguntas[perguntaAtual].Id,
                RespostaSimNao = resposta,
                ValorResposta = resposta ? 1.0 : 0.0 
            });

            perguntaAtual++;
            MostrarPerguntaAtual();
        }
    }

}