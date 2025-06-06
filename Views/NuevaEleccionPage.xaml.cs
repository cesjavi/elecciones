using Elecciones.Models;

namespace Elecciones.Views;

public partial class NuevaEleccionPage : ContentPage
{
    public NuevaEleccionPage()
    {
        InitializeComponent();
        CargarElecciones();
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var nombre = nombreEleccionEntry.Text;
        var fecha = fechaPicker.Date;

        if (string.IsNullOrWhiteSpace(nombre))
        {
            await DisplayAlert("Error", "Debe ingresar un nombre para la elección.", "OK");
            return;
        }

        using var db = new AppDbContext();
        var nueva = new Eleccion { Nombre = nombre, Fecha = fecha };
        db.Elecciones.Add(nueva);
        await db.SaveChangesAsync();

        nombreEleccionEntry.Text = "";
        fechaPicker.Date = DateTime.Now;

        estadoLabel.Text = "Elección guardada correctamente.";
        estadoLabel.IsVisible = true;
        CargarElecciones();

    }
    private void CargarElecciones()
    {
        using var db = new AppDbContext();
        var elecciones = db.Elecciones.OrderByDescending(e => e.Fecha).ToList();
        eleccionesView.ItemsSource = elecciones;
    }

    private async void OnEliminarEleccionClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is Eleccion eleccion)
        {
            bool confirmar = await DisplayAlert("Eliminar", $"¿Eliminar la elección \"{eleccion.Nombre}\"?", "Sí", "No");
            if (!confirmar) return;

            using var db = new AppDbContext();
            var item = db.Elecciones.FirstOrDefault(e => e.Id == eleccion.Id);

            if (item != null)
            {
                db.Elecciones.Remove(item);
                await db.SaveChangesAsync();
                await DisplayAlert("Eliminada", "La elección fue eliminada correctamente.", "OK");
                CargarElecciones();
            }
        }
    }

}