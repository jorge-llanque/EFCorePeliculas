using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCorePeliculas.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool EnCartelera { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string PosterURL { get; set; }
        public List<Genero> Generos { get; set; }
        public List<SalaDeCine> SalasDeCine { get; set; }
        public List<PeliculaActor> PeliculasActores { get; set; }
    }
}
