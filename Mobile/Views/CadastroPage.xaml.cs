using Mobile.Models;
using Mobile.Services;

namespace Mobile.Views;

public partial class CadastroPage : ContentPage
{
    private readonly ApiService _apiService;

    public static int UsuarioId { get; private set; } // Armazena o ID global do usu�rio (se necess�rio)

    public CadastroPage()
    {
        InitializeComponent();
        _apiService = new ApiService(new HttpClient());
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = new Usuario
            {
                Nome = entryNome.Text,
                Idade = int.Parse(entryIdade.Text),
                Sexo = pickerSexo.SelectedItem?.ToString()
            };

            if (string.IsNullOrWhiteSpace(usuario.Nome) ||
                usuario.Idade <= 0 ||
                string.IsNullOrWhiteSpace(usuario.Sexo))
            {
                await DisplayAlert("Erro", "Preencha todos os campos corretamente", "OK");
                return;
            }

            // ? Envia para a API e salva o ID retornado
            int id = await _apiService.CadastrarUsuario(usuario);
            UsuarioId = id;

            await DisplayAlert("Sucesso", $"Usu�rio cadastrado com ID: {id}", "OK");

            // ? Navega para a aba "Score"
            if (Parent is TabbedPage tabbedPage)
            {
                tabbedPage.CurrentPage = tabbedPage.Children[2]; // �ndice da aba "Score"
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }
}
