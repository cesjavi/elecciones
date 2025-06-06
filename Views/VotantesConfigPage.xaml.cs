using Elecciones.Models;
using System.Text;

namespace Elecciones.Views;

public partial class VotantesConfigPage : ContentPage
{
    private Eleccion eleccionSeleccionada;

    public VotantesConfigPage()
    {
        InitializeComponent();
        CargarElecciones();
    }

    private void CargarElecciones()
    {
        using var db = new AppDbContext();
        var elecciones = db.Elecciones.ToList();
        eleccionPicker.ItemsSource = elecciones;
    }

    private void OnEleccionSeleccionada(object sender, EventArgs e)
    {
        eleccionSeleccionada = eleccionPicker.SelectedItem as Eleccion;
    }
    private async void OnGuardarVotanteClicked(object sender, EventArgs e)
    {
        if (eleccionSeleccionada == null)
        {
            await DisplayAlert("Error", "Debe seleccionar una elección primero.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(nombreEntry.Text) ||
            string.IsNullOrWhiteSpace(apellidoEntry.Text) ||
            string.IsNullOrWhiteSpace(dniEntry.Text) ||
            string.IsNullOrWhiteSpace(numeroOrdenEntry.Text))
        {
            await DisplayAlert("Error", "Complete todos los campos.", "OK");
            return;
        }

        if (!int.TryParse(numeroOrdenEntry.Text, out var nroOrden))
        {
            await DisplayAlert("Error", "El número de orden debe ser un número válido.", "OK");
            return;
        }

        var votante = new Votante
        {
            Nombre = nombreEntry.Text.Trim(),
            Apellido = apellidoEntry.Text.Trim(),
            DNI = dniEntry.Text.Trim(),
            NumeroOrden = nroOrden,
            IdEleccion = eleccionSeleccionada.Id,
            HaVotado = false
        };

        using var db = new AppDbContext();
        db.Votantes.Add(votante);
        await db.SaveChangesAsync();

        await DisplayAlert("Éxito", "Votante agregado correctamente.", "OK");

        nombreEntry.Text = "";
        apellidoEntry.Text = "";
        dniEntry.Text = "";
        numeroOrdenEntry.Text = "";
    }

    private async void OnImportarCSVClicked(object sender, EventArgs e)
    {
        var resultado = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle = "Selecciona un archivo CSV",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".csv" } },
                { DevicePlatform.Android, new[] { "text/csv" } },
                { DevicePlatform.iOS, new[] { "public.comma-separated-values-text" } }
            })

        });

        if (resultado == null)
            return;

        try
        {
            var contenido = await File.ReadAllTextAsync(resultado.FullPath, Encoding.UTF8);
            var lineas = contenido.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var nuevosVotantes = new List<Votante>();

            foreach (var linea in lineas.Skip(1)) // omitir encabezado
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                var campos = linea.Trim().Split(',');

                if (campos.Length < 4) continue;

                var votante = new Votante
                {
                    Nombre = campos[0].Trim(),
                    Apellido = campos[1].Trim(),
                    DNI = campos[2].Trim(),
                    NumeroOrden = int.TryParse(campos[3].Trim(), out var orden) ? orden : 0,
                    HaVotado = false,
                    IdEleccion = eleccionSeleccionada?.Id ?? 0 // Se requiere elección seleccionada
                };

                nuevosVotantes.Add(votante);
            }

            using var db = new AppDbContext();
            db.Votantes.AddRange(nuevosVotantes);
            await db.SaveChangesAsync();

            await DisplayAlert("Importación completada", $"{nuevosVotantes.Count} votantes importados.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Hubo un problema al importar: {ex.Message}", "OK");
        }
    }
    private void CargarVotantes()
    {
        if (eleccionSeleccionada == null)
        {
            votantesView.ItemsSource = null;
            return;
        }

        using var db = new AppDbContext();
        var lista = db.Votantes
            .Where(v => v.IdEleccion == eleccionSeleccionada.Id)
            .OrderBy(v => v.NumeroOrden)
            .ToList();

        votantesView.ItemsSource = lista.Select(v => new VotanteViewModel(v)).ToList();
    }
    private async void OnEliminarVotanteClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is VotanteViewModel vm)
        {
            var confirm = await DisplayAlert("Eliminar", $"¿Eliminar a {vm.NombreCompleto}?", "Sí", "No");
            if (!confirm) return;

            using var db = new AppDbContext();
            var votante = db.Votantes.FirstOrDefault(v => v.Id == vm.Id);
            if (votante != null)
            {
                db.Votantes.Remove(votante);
                await db.SaveChangesAsync();
            }

            CargarVotantes();
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarElecciones();
    }

    private async void OnEditarVotanteClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is VotanteViewModel vm)
        {
            var nuevoNombre = await DisplayPromptAsync("Editar", "Nuevo nombre:", initialValue: vm.Nombre);
            if (nuevoNombre == null) return;

            using var db = new AppDbContext();
            var votante = db.Votantes.FirstOrDefault(v => v.Id == vm.Id);
            if (votante != null)
            {
                votante.Nombre = nuevoNombre;
                await db.SaveChangesAsync();
            }

            CargarVotantes();
        }
    }    
}
