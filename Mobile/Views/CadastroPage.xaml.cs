using Mobile.Models;
using Mobile.Services;

namespace Mobile.Views;

public partial class CadastroPage : ContentPage
{
    public CadastroPage()
    {
        InitializeComponent();
    }

    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        try
        {
            var usuario = new Models.Usuario
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

            
            if (Parent is TabbedPage tabbedPage)
            {
                tabbedPage.CurrentPage = tabbedPage.Children[2]; 
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }
}
