using Microsoft.Extensions.Logging;
using Mobile.Services;
using Mobile.Views;
using System.Net.Http.Headers;

namespace Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { /* ... */ });

        
        builder.Services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri("https://five-dsm-pi-backend.onrender.com");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });

       
        builder.Services.AddTransient<TestePage>(); 
        builder.Services.AddSingleton<MenuPrincipal>();
        builder.Services.AddSingleton<ApiService>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}