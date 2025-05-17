using Mobile.Models;

namespace Mobile.Views;

public partial class TestePage : ContentPage
{
    private List<Models.Pergunta> perguntas;
    private int perguntaAtual = 0;

    public TestePage()
    {
        InitializeComponent();
        CarregarPerguntasTeste(); 
    }

    
    private void CarregarPerguntasTeste()
    {
        perguntas = new List<Models.Pergunta>
        {
            new Models.Pergunta { Id = 1, TextoPergunta = "Você possui conta bancária?" },
            new Models.Pergunta { Id = 2, TextoPergunta = "Possui cartão de crédito?" },
           
        };

        MostrarPerguntaAtual();
    }

    private void MostrarPerguntaAtual()
    {
        if (perguntaAtual < perguntas.Count)
        {
            lblPergunta.Text = perguntas[perguntaAtual].TextoPergunta;
        }
        else
        {
            
            FinalizarTeste();
        }
    }

    private async void FinalizarTeste()
    {
        await DisplayAlert("Sucesso", "Teste concluído!", "OK");
        
        if (Parent is TabbedPage tabbedPage)
        {
            tabbedPage.CurrentPage = tabbedPage.Children[3]; 
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
        

        perguntaAtual++;
        MostrarPerguntaAtual();
    }
}
