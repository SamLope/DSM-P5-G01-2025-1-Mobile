using Mobile.Services;

namespace Mobile.Views;

public partial class ResultadoPage : ContentPage
{
    public ResultadoPage()
    {
        InitializeComponent();
        Appearing += ResultadoPage_Appearing;
    }

    private async void ResultadoPage_Appearing(object sender, EventArgs e)
    {
        await ObterResultadoComEspera(App.CurrentUserId);
    }

    private async Task ObterResultadoComEspera(int usuarioId)
    {
        int tentativas = 0;
        const int maxTentativas = 10;
        const int intervaloMs = 1000;

        while (tentativas < maxTentativas)
        {
            try
            {
                // Pega resultado e nome do usuário atual 
                var (resultado, nome) = await App.ApiClient.ObterUltimoResultadoAsync(usuarioId);

                //  Exibe tudo direto com nome e resultado
                MostrarResultado(resultado, nome);

                

                return;
            }
            catch (Exception ex)
            {
                tentativas++;
                Console.WriteLine($"Tentativa {tentativas}: resultado ainda não disponível.");
                await Task.Delay(intervaloMs);
            }
        }

        await DisplayAlert("Erro", "Não foi possível recuperar o resultado. Tente novamente mais tarde.", "OK");
    }


    private async void MostrarResultado(int resultado, string nome)
    {
        labelNome.Text = $"{nome} seu Score é:";

        string mensagem, corFundo, corTexto, imagem;

        switch (resultado)
        {
            case 2:
                labelResultado.Text =  "Excelente!";
                mensagem = "Parabéns! Seu score está excelente.";
                imagem = "logogreen.png";
                corFundo = "#C8E6C9";
                corTexto = "#2E7D32";
                break;
            case 1:
                labelResultado.Text = " Médio!";
                mensagem = "Você está no caminho certo. Continue melhorando!";
                imagem = "logoyellow.png";
                corFundo = "#FFF9C4";
                corTexto = "#FBC02D";
                break;
            case 0:
                labelResultado.Text = "Ruim!";
                mensagem = "Não desanime. Você pode melhorar!";
                imagem = "logored.png";
                corFundo = "#FFCDD2";
                corTexto = "#C62828";
                break;
            default:
                labelResultado.Text = "Desconhecido";
                mensagem = "Não foi possível determinar seu score.";
                imagem = "logoscore.png";
                corFundo = "White";
                corTexto = "Gray";
                break;
        }

        labelResultado.TextColor = Color.FromArgb(corTexto);
        labelMensagem.Text = mensagem;
        resultadoPage.BackgroundColor = Color.FromArgb(corFundo);
        imageResultado.Source = imagem;

        // animação
        imageResultado.Opacity = 0;
        imageResultado.Scale = 0.8;
        await imageResultado.FadeTo(1, 1000);
        await imageResultado.ScaleTo(1, 300, Easing.BounceOut);
    }


    
}
