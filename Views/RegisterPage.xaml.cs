using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Elecciones.Services;
using Microsoft.Maui.ApplicationModel;

namespace Elecciones.Views;

public partial class RegisterPage : ContentPage
{
    private readonly AuthService _authService;

    public RegisterPage()
    {
        InitializeComponent();
        _authService = IPlatformApplication.Current?.Services?.GetService<AuthService>();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text?.Trim();
        var dni = dniEntry.Text?.Trim();
        var password = passwordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(dni))
        {
            await DisplayAlert("Error", "Ingrese usuario, DNI y contraseña", "OK");
            return;
        }

        var success = await _authService.RegisterAsync(username, password, dni);
        if (success)
        {
            Preferences.Set("username", username);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            await DisplayAlert("Advertencia", "El usuario ya existe", "OK");
        }
    }
}
