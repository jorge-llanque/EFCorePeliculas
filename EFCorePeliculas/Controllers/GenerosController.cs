﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCorePeliculas.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas.Controllers
{
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly AppDbContext context;

        public GenerosController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Genero>> Get()
        {
            return await context.generos.OrderBy(g => g.Nombre).AsNoTracking().ToListAsync();
        }

        [HttpGet("primer")]
        public async Task<ActionResult<Genero>> Primer()
        {
            var genero = await context.generos.FirstOrDefaultAsync(g => g.Nombre.StartsWith("a"));
            if (genero is null)
            {
                return NotFound();
            }
            return genero;
        }

        [HttpGet("id:int")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await context.generos.FirstOrDefaultAsync(data => data.Identificador == id);
            if(genero is null)
            {
                return NotFound();
            }
            return genero;

        }

        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> Filtrar(string nombre)
        {
            return await context.generos.Where(
                g => g.Nombre.StartsWith("C") 
                || g.Nombre.StartsWith("A")
                ).ToListAsync();
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetPaginacion(int pagina =1)
        {
            var cantidadRegistrosPorPagina = 2;
            var generos = await context.generos.Skip((pagina - 1) * cantidadRegistrosPorPagina)
                .Take(cantidadRegistrosPorPagina)
                .ToListAsync();
            return generos;
        }
    }
}
