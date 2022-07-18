using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCorePeliculas.Entidades;

namespace EFCorePeliculas.Dto
{
    public class SalaDeCineCreacionDto
    {
        public decimal Precio { get; set; }
        public TipoSalaDeCine TipoSalaDeCine { get; set; }
    }
}
