using Microsoft.Maui.Storage;
using System.Text;
using Elecciones.Models;
using Elecciones;
using System.Linq;

namespace Elecciones.Views;

public partial class ConfiguracionPage : TabbedPage
{
    private Eleccion eleccionSeleccionada;
    public ConfiguracionPage()
    {
        InitializeComponent();
    }
    
}