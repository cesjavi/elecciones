using Elecciones.Views;

namespace Elecciones;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnEleccionesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EleccionesPage());
    }

    private async void OnConfiguracionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ConfiguracionPage());
    }
}
