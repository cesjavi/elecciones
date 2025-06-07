using Microsoft.Maui.Controls;

namespace Elecciones.Views;

public partial class AnalisisPage : ContentPage
{
    public AnalisisPage(int total, int votaron)
    {
        InitializeComponent();

        totalLabel.Text = $"Total de votantes: {total}";
        votaronLabel.Text = $"Votaron: {votaron}";
        double porcentaje = total > 0 ? votaron * 100.0 / total : 0;
        porcentajeLabel.Text = $"Porcentaje: {porcentaje:F2}%";
    }
}
