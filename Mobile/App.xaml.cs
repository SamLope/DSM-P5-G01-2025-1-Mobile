namespace Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Definir MainPage
        MainPage = new MenuPrincipal(); // ou outra página inicial
    }
}
