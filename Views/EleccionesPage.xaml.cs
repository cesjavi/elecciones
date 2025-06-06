namespace Elecciones.Views;

public partial class EleccionesPage : ContentPage
{
    public EleccionesPage()
    {
        InitializeComponent();
        CargarElecciones();
    }

    private void CargarElecciones()
    {
        using var db = new AppDbContext();
        var lista = db.Elecciones
                      .OrderByDescending(e => e.Fecha)
                      .ToList();
        eleccionesView.ItemsSource = lista;
    }

    private async void OnEleccionSeleccionada(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Eleccion eleccion)
        {
            // Acá luego navegaremos a la pantalla de votantes:
            await DisplayAlert("Elección seleccionada", $"ID: {eleccion.Id}\nNombre: {eleccion.Nombre}", "OK");

            // Luego: await Navigation.PushAsync(new VotantesPage(eleccion));
        }

        // Limpiar selección
        eleccionesView.SelectedItem = null;
    }
}