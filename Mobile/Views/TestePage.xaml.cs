using System.Text.Json;
using Mobile.ViewModels;
using Mobile.Services;
using Mobile.Models;

namespace Mobile.Views;

public partial class TestePage : ContentPage
{
    private readonly TesteViewModel _viewModel;
    private readonly LocalStorageService _storageService;

    public TestePage()
    {
        InitializeComponent();

        _viewModel = new TesteViewModel();
        _storageService = new LocalStorageService();
        BindingContext = _viewModel;

        CarregarDadosIniciais();
    }

    private async void CarregarDadosIniciais()
    {
        try
        {
            loadingIndicator.IsVisible = true;

            // Carrega respostas salvas localmente
            var respostasSalvas = await _storageService.LoadRespostasAsync();
            if (respostasSalvas.Any())
            {
                _viewModel.Respostas.AddRange(respostasSalvas);
                _viewModel.VerificarCompletude();
            }

            await CarregarPerguntasTeste();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar dados: {ex.Message}", "OK");
        }
        finally
        {
            loadingIndicator.IsVisible = false;
        }
    }

    private async Task CarregarPerguntasTeste()
    {
        try
        {
            _viewModel.Perguntas.Clear();

            // 1. Chama API e obtem as perguntas
            var perguntasApi = await App.ApiClient.GetPerguntasAsync();

            // 2. Ordena pelo id crescente
            var perguntasOrdenadas = perguntasApi.OrderBy(p => p.Id).ToList();

            // 3. Adiciona no ObservableCollection para o CarouselView
            foreach (var pergunta in perguntasOrdenadas)
            {
                _viewModel.Perguntas.Add(pergunta);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao carregar perguntas: {ex.Message}", "OK");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        PerguntasCarousel.Scrolled += OnCarouselScrolled;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        PerguntasCarousel.Scrolled -= OnCarouselScrolled;
    }

    private void OnCarouselScrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        var currentItem = PerguntasCarousel.CurrentItem as Pergunta;
        if (currentItem != null)
        {
            AtualizarControlesResposta(currentItem);
        }
    }

    private void AtualizarControlesResposta(Pergunta pergunta)
    {
        var stackLayout = (PerguntasCarousel.CurrentItem as VisualElement)?.Parent as StackLayout;
        var container = stackLayout?.FindByName<ContentView>("RespostaContainer");

        if (container == null) return;

        container.Content = pergunta.TextoPergunta switch
        {
            string t when t.Contains("Sim/Não") => CriarSimNaoButtons(pergunta),
            _ => CriarEntryNumeric(pergunta)
        };

        // Preenche resposta existente
        var respostaExistente = _viewModel.Respostas.FirstOrDefault(r => r.IdPergunta == pergunta.Id);
        if (respostaExistente != null)
        {
            if (container.Content is Entry entry)
            {
                entry.Text = respostaExistente.ValorResposta.ToString();
            }
            else if (container.Content is Grid grid)
            {
                // Lógica para selecionar o botão Sim/Não se aplicável
            }
        }
    }

    private View CriarEntryNumeric(Pergunta pergunta)
    {
        var entry = new Entry
        {
            Keyboard = Keyboard.Numeric,
            Placeholder = "Digite um valor",
            HorizontalOptions = LayoutOptions.Center,
            Style = (Style)Application.Current.Resources["PrimaryEntry"]
        };

        entry.TextChanged += (s, e) =>
            RegistrarResposta(pergunta, e.NewTextValue);

        return entry;
    }

    private View CriarSimNaoButtons(Pergunta pergunta)
    {
        var grid = new Grid { ColumnSpacing = 20 };
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

        var btnSim = new Button
        {
            Text = "Sim",
            Style = (Style)Application.Current.Resources["PrimaryButton"]
        };

        var btnNao = new Button
        {
            Text = "Não",
            Style = (Style)Application.Current.Resources["SecondaryButton"]
        };

        btnSim.Clicked += (s, e) => RegistrarResposta(pergunta, "true");
        btnNao.Clicked += (s, e) => RegistrarResposta(pergunta, "false");

        grid.Children.Add(btnSim);
        grid.Children.Add(btnNao);
        Grid.SetColumn(btnNao, 1);

        return grid;
    }

    private void RegistrarResposta(Pergunta pergunta, string resposta)
    {
        var existing = _viewModel.Respostas.FirstOrDefault(r => r.IdPergunta == pergunta.Id);

        if (existing != null)
        {
            _viewModel.Respostas.Remove(existing);
        }

        _viewModel.Respostas.Add(new Resposta
        {
            IdPergunta = pergunta.Id,
            IdUsuario = App.CurrentUserId,
            ColunaDataSet = pergunta.ColunaDataset, // usa o campo vindo da API
            ValorResposta = double.TryParse(resposta, out var num) ? num : (resposta.ToLower() == "true" ? 1 : 0),
            RespostaSimNao = resposta.ToLower() == "true" || resposta.ToLower() == "false"
        });

        _viewModel.VerificarCompletude();

        // Salva localmente após cada resposta
        _ = _storageService.SaveRespostasAsync(_viewModel.Respostas);
    }

    private async void OnFinalizarClicked(object sender, EventArgs e)
    {
        if (!_viewModel.TodasRespondidas)
        {
            await DisplayAlert("Atenção", "Responda todas as perguntas antes de finalizar", "OK");
            return;
        }

        try
        {
            loadingIndicator.IsVisible = true;

            // 1. Salva localmente
            await _storageService.SaveRespostasAsync(_viewModel.Respostas);

            // 2. Prepara para enviar à API
            var json = JsonSerializer.Serialize(new { respostas = _viewModel.Respostas });

            // 3. DEBUG: Mostra o JSON (remover na versão final)
            await DisplayAlert("DEBUG", $"Dados prontos para API:\n{json}", "OK");

            // 4. Envia para API (implementar quando pronto)
            // await App.ApiClient.EnviarRespostasEmLote(_viewModel.Respostas);

            // 5. Navega para ResultadoPage
            if (Parent is TabbedPage tabbedPage)
            {
                tabbedPage.CurrentPage = tabbedPage.Children[3]; // Índice da ResultadoPage
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao finalizar: {ex.Message}", "OK");
        }
        finally
        {
            loadingIndicator.IsVisible = false;
        }
    }
}
