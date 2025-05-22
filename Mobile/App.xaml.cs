using Mobile.Services;

namespace Mobile;

public partial class App : Application
{
    public static ApiService ApiClient { get; private set; }
    public static int CurrentUserId { get; set; }

    public App(ApiService apiService) 
    {
        InitializeComponent();
        ApiClient = apiService; 

        MainPage = new MenuPrincipal();
    }
}