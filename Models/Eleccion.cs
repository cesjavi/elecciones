using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Eleccion
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; }
    public DateTime Fecha { get; set; }
    [NotMapped] // opcional si usás EF
    public string NombreConFecha => $"{Nombre} ({Fecha:dd/MM/yyyy})";

    public List<Votante> Votantes { get; set; } = new();
}
