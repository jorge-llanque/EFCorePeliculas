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
    [Route("api/autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public AutoresController(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDto>> Get()
        {
            return await context.actores.ProjectTo<ActorDto>(mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ActorCreacionDto actorCreacionDto, int id)
        {
            var actorDb = await context.actores.AsTracking().FirstOrDefaultAsync(a => a.Id == id);

            if (actorDb is null)
            {
                return NotFound();
            }

            actorDb = mapper.Map(actorCreacionDto, actorDb);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("desconectado/{id:int}")]
        public async Task<ActionResult> PutDesconectado(ActorCreacionDto actorCreacionDto, int id)
        {
            var existeActor = await context.actores.AnyAsync(a => a.Id == id);
            if (!existeActor)
            {
                return NotFound();
            }

            var actor = mapper.Map<Actor>(actorCreacionDto);
            actor.Id = id;

            context.Update(actor);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
