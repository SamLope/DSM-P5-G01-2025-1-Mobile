using Mobile.Models;
using Mobile.Services;

namespace Mobile.Views;

public partial class CadastroPage : ContentPage
{
    private readonly ApiService _apiService;

    public static int UsuarioId { get; private set; } // Armazena o ID global do usuário 

    public CadastroPage()
    {
        InitializeComponent();
        _apiService = new ApiService(new HttpClient());

        // Reseta o usuário ao entrar na tela
        UsuarioId = 0;
        App.CurrentUserId = 0;
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = new Usuario
            {
                Nome = entryNome.Text,
                Sexo = pickerSexo.SelectedItem?.ToString()
            };

            if (string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Sexo))
            {
                await DisplayAlert("Erro", "Preencha todos os campos", "OK");
                return;
            }

            int id = await _apiService.CadastrarUsuario(usuario);
            UsuarioId = id;
            App.CurrentUserId = id;

            await DisplayAlert("Sucesso", $"Usuário cadastrado com ID: {id}", "OK");

            if (Parent is TabbedPage tabbedPage)
            {
                tabbedPage.CurrentPage = tabbedPage.Children[2]; // Vai para TestePage
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }
}
