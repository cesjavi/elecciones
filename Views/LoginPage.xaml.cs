using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using Elecciones.Services;
using Microsoft.Maui.ApplicationModel;


namespace Elecciones.Views;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;

    public LoginPage()
    {
        InitializeComponent();
        _authService = IPlatformApplication.Current?.Services?.GetService<AuthService>();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text?.Trim();
        var password = passwordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Error", "Ingrese usuario y contraseña", "OK");
            return;
        }

        var user = await _authService.LoginAsync(username, password);
        if (user != null)
        {
            Preferences.Set("username", user.Username);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Credenciales inválidas", "OK");
        }
    }


    private async void OnNavigateRegister(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
