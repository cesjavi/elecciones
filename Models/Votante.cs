using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Votante
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Eleccion))]
    public int IdEleccion { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string DNI { get; set; }

    public int NumeroOrden { get; set; }

    public bool HaVotado { get; set; }

    public Eleccion Eleccion { get; set; }
}
