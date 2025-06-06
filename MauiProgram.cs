using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Maui;
using System.Globalization;


namespace Elecciones
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-AR");

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() // Corrected method for CommunityToolkit.Maui  
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
