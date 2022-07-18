using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculas.Dto;
using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Controllers
{
    [Route("api/peliculas")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PeliculaDto>> Get(int id)
        {
            var pelicula = await context.peliculas
                .Include(p => p.Generos)
                .Include(p => p.SalasDeCine)
                    .ThenInclude(s => s.Cine)
                .Include(p => p.PeliculasActores)
                    .ThenInclude(pa => pa.Actor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            var peliculaDto = mapper.Map<PeliculaDto>(pelicula);

            return peliculaDto;
        }

        [HttpGet("ProjectTo/{id:int}")]
        public async Task<ActionResult<PeliculaDto>> GetProjectTo(int id)
        {
            var pelicula = await context.peliculas
                .ProjectTo<PeliculaDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula is null)
            {
                return NotFound();
            }

            var peliculaDto = mapper.Map<PeliculaDto>(pelicula);

            return peliculaDto;
        }

        [HttpGet("CargadoSelectivo/{id:int}")]
        public async Task<ActionResult> GetSelectivo(int id)
        {
            var pelicula = await context.peliculas.Select(p =>
            new
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Generos = p.Generos.OrderByDescending(g => g.Nombre).Select(g => g.Nombre).ToList(),
                CantidadActores = p.PeliculasActores.Count(),
                CantidadCines = p.SalasDeCine.Select(s => s.CineId).Distinct().Count(),
            }).FirstOrDefaultAsync();

            if (pelicula is null)
            {
                return NotFound();
            }
            return Ok(pelicula);
        }
        
        [HttpGet("cargadoExplicito/{id:int}")]
        public async Task<ActionResult<PeliculaDto>> GetExplicito(int id)
        {
            var pelicula = await context.peliculas.AsTracking().FirstOrDefaultAsync(p => p.Id == id);

            //...
            await context.Entry(pelicula).Collection(p => p.Generos).LoadAsync();
            if (pelicula is null)
            {
                return NotFound();
            }

            var peliculaDto = mapper.Map<PeliculaDto>(pelicula);
            return peliculaDto;
        }

        [HttpGet("agrupadasPorGenero")]
        public async Task<ActionResult> GetAgrupadasPorCartelera()
        {
            var peliculasAgrupadas = await context.peliculas.GroupBy(p => p.EnCartelera)
                .Select(g => new
                {
                    EnCartelera = g.Key,
                    Conteo = g.Count(),
                    Peliculas = g.ToList()
                }).ToListAsync();

            return Ok(peliculasAgrupadas);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<PeliculaDto>>> Filtrar([FromQuery] PeliculasFiltroDto peliculasFiltroDto)
        {
            var peliculasQueryable = context.peliculas.AsQueryable();

            if (!string.IsNullOrEmpty(peliculasFiltroDto.Titulo))
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.Titulo.Contains(peliculasFiltroDto.Titulo));
            }
            if (peliculasFiltroDto.EnCartelera)
            {
                peliculasQueryable = peliculasQueryable.Where(p => p.EnCartelera);
            }
            if (peliculasFiltroDto.ProximosEstrenos)
            {
                var hoy = DateTime.Today;
                peliculasQueryable = peliculasQueryable.Where(p => p.FechaEstreno > hoy);
            }
            if (peliculasFiltroDto.GeneroId != 0)
            {
                peliculasQueryable = peliculasQueryable.Where(p =>
                p.Generos.Select(g => g.Identificador)
                .Contains(peliculasFiltroDto.GeneroId));
            }

            var peliculas = await peliculasQueryable.Include(p => p.Generos).ToListAsync();

            return mapper.Map<List<PeliculaDto>>(peliculas);
        }

        [HttpPost("InsertarRegistroDataRelacionadaExistente")]
        public async Task<ActionResult> Post(PeliculaCreacionDto peliculaCreacionDto)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDto);
            pelicula.Generos.ForEach(g => context.Entry(g).State = EntityState.Unchanged);
            pelicula.SalasDeCine.ForEach(s => context.Entry(s).State = EntityState.Unchanged);

            if (pelicula.PeliculasActores is not null)
            {
                for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
                {
                    pelicula.PeliculasActores[i].Orden = i + 1;
                }
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
