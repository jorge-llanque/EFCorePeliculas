using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Dto
{
    public class CineCreacionDto
    {
        [Required]
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public CineOfertaCreacionDto CineOferta { get; set; }
        public SalaDeCineCreacionDto[] SalasDeCine { get; set; }
    }
}
