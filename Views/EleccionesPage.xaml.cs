using Elecciones.Models;

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
            await Navigation.PushAsync(new VotantesPage(eleccion));
        }

        // Limpiar selección
        eleccionesView.SelectedItem = null;
    }
}