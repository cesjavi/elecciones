using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elecciones.Models
{
    public class VotanteViewModel
    {
        public int Id { get; set; }
        public string NombreCompleto => $"{Nombre} {Apellido}";
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public int NumeroOrden { get; set; }
        public bool HaVotado { get; set; }

        public VotanteViewModel(Votante v)
        {
            Id = v.Id;
            Nombre = v.Nombre;
            Apellido = v.Apellido;
            DNI = v.DNI;
            NumeroOrden = v.NumeroOrden;
            HaVotado = v.HaVotado;
        }
    }
}
