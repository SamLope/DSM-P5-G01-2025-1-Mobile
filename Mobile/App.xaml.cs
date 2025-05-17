using Mobile.Services;

namespace Mobile;

public partial class App : Application
{
    public static ApiService ApiClient { get; private set; }
    public static int CurrentUserId { get; set; }

    public App()
    {
        InitializeComponent();
        ApiClient = new ApiService("http://10.0.2.2:3000");
        
        MainPage = new MenuPrincipal(); 
        
    }
}
