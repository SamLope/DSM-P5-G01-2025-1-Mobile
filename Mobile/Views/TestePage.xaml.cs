using System.Text.Json;
using Mobile.ViewModels;
using Mobile.Services;
using Mobile.Models;

namespace Mobile.Views
{
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

                var perguntasApi = await App.ApiClient.GetPerguntasAsync();

                var perguntasOrdenadas = perguntasApi.OrderBy(p => p.Id).ToList();

                foreach (var pergunta in perguntasOrdenadas)
                {
                    _viewModel.Perguntas.Add(pergunta);
                }

                _viewModel.IniciarQuestionario();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha ao carregar perguntas: {ex.Message}", "OK");
            }
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_viewModel.PerguntaAtual != null)
            {
                var textoResposta = e.NewTextValue;

                RegistrarResposta(_viewModel.PerguntaAtual, textoResposta);

                BotaoAvancar.IsEnabled = !string.IsNullOrWhiteSpace(textoResposta);
            }
        }

        private void OnAvancarClicked(object sender, EventArgs e)
        {
            _viewModel.IrParaProximaPergunta();

            BotaoAvancar.IsEnabled = !(_viewModel.PerguntaAtual.ValorDigitado == null);

            BotaoFinalizar.IsVisible = _viewModel.EhUltimaPergunta();
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
                ColunaDataSet = pergunta.ColunaDataset,
                ValorResposta = double.TryParse(resposta, out var num) ? num : 0,
                RespostaSimNao = false
            });

            _viewModel.VerificarCompletude();

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

                // 2. Prepara objeto RespostasAgregadas com as respostas
                var respostasAgregadas = new RespostasAgregadas
                {
                    IdUsuario = App.CurrentUserId,
                    Resposta01 = ObterValorResposta(1),
                    Resposta02 = ObterValorResposta(2),
                    Resposta03 = ObterValorResposta(3),
                    Resposta04 = ObterValorResposta(4),
                    Resposta05 = ObterValorResposta(5),
                    Resposta06 = ObterValorResposta(6),
                    Resposta07 = ObterValorResposta(7),
                    Resposta08 = ObterValorResposta(8),
                    Resposta09 = ObterValorResposta(9),
                    Resposta10 = ObterValorResposta(10)
                };

                // 3. Serializa para JSON
                var json = JsonSerializer.Serialize(respostasAgregadas);

                // 4. MOSTRA no Console para ver se está correto
                Console.WriteLine("Payload enviado: " + json);

                // 5. Envia para API
                await App.ApiClient.EnviarRespostasAgregadas(respostasAgregadas);

                // 6. Mensagem de sucesso (opcional)
                await DisplayAlert("Sucesso", "Respostas enviadas com sucesso!", "OK");

                // 7. Vai para ResultadoPage
                if (Parent is TabbedPage tabbedPage)
                {
                    tabbedPage.CurrentPage = tabbedPage.Children[3]; // ResultadoPage
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



        private string ObterValorResposta(int idPergunta)
        {
            var resposta = _viewModel.Respostas.FirstOrDefault(r => r.IdPergunta == idPergunta);
            return resposta != null ? resposta.ValorResposta.ToString() : "";
        }
    }
}
