using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Dto
{
    public class PeliculaCreacionDto
    {
        public string Titulo { get; set; }
        public bool EnCartelera { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<int> Generos { get; set; }
        public List<int> SalasDeCine { get; set; }
        public List<PeliculaActorCreacionDto> PeliculasActores { get; set; }
    }
}
