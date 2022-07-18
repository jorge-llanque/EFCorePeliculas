using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Dto
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public ICollection<GeneroDto> Generos { get; set; }
        public ICollection<CineDto> Cines { get; set; }
        public ICollection<ActorDto> Actores { get; set; }
    }
}
