using Elecciones.Models;

namespace Elecciones.Views;

public partial class VotantesPage : ContentPage
{
    private readonly Eleccion eleccion;
    private List<Votante> votantes = new();

    public VotantesPage(Eleccion eleccion)
    {
        InitializeComponent();
        this.eleccion = eleccion;
        Title = $"Votantes - {eleccion.Nombre}";
        CargarVotantes();
    }

    private void CargarVotantes()
    {
        using var db = new AppDbContext();
        votantes = db.Votantes
            .Where(v => v.IdEleccion == eleccion.Id)
            .OrderBy(v => v.NumeroOrden)
            .ToList();

        votantesView.ItemsSource = votantes
            .Select(v => new VotanteViewModel(v))
            .ToList();
    }
    private async void OnEstadisticasClicked(object sender, EventArgs e)
    {
        int total = votantes.Count;
        int votaron = votantes.Count(v => v.HaVotado);
        double porcentaje = total > 0 ? (votaron * 100.0 / total) : 0;

        await DisplayAlert("Estad�sticas",
            $"Total de votantes: {total}\n" +
            $"Votaron: {votaron}\n" +
            $"Porcentaje: {porcentaje:F2}%",
            "OK");
    }

    private void OnBuscarChanged(object sender, TextChangedEventArgs e)
    {
        var texto = e.NewTextValue?.ToLower() ?? "";
        var filtrados = votantes
            .Where(v =>
                (v.Nombre + " " + v.Apellido).ToLower().Contains(texto) ||
                v.DNI.Contains(texto) ||
                v.NumeroOrden.ToString().Contains(texto))
            .Select(v => new VotanteViewModel(v))
            .ToList();

        votantesView.ItemsSource = filtrados;
    }

    private void OnCheckChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox cb && cb.BindingContext is VotanteViewModel vm)
        {
            using var db = new AppDbContext();
            var votante = db.Votantes.FirstOrDefault(v => v.Id == vm.Id);
            if (votante != null)
            {
                votante.HaVotado = e.Value;
                db.SaveChanges();
            }
        }
    }
}
