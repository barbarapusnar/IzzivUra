using Microsoft.Extensions.Logging;
using Auth0.OidcClient;
using Microsoft.Extensions.Http;
namespace ShranjevanjeZetonaMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "dev-yltaj4tpoilt3zx1.us.auth0.com",
                ClientId = "QEVMGduHc8kAJ137YwPe8bv5muK4HZpN",
                RedirectUri = "myapp://callback",
                PostLogoutRedirectUri = "myapp://callback",
                Scope = "openid profile email"
            }));

            builder.Services.AddSingleton<TokenHandler>();
            builder.Services.AddHttpClient("DemoAPI",
                    client => client.BaseAddress = new Uri("https://localhost:6061/")
                ).AddHttpMessageHandler<TokenHandler>();
            builder.Services.AddTransient(
                sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("DemoAPI")
            );

            return builder.Build();
        }
    }
}
