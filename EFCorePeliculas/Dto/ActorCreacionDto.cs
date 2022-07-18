using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Dto
{
    public class ActorCreacionDto
    {
        public string Nombre { get; set; }
        public string Biografia { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}
