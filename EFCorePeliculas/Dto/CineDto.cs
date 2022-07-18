using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Dto
{
    public class CineDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
    }
}
