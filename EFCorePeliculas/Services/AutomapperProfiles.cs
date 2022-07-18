using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EFCorePeliculas.Dto;
using EFCorePeliculas.Entidades;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCorePeliculas.Services
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Actor, ActorDto>();

            CreateMap<Cine, CineDto>()
                .ForMember(dto => dto.Latitud, ent => ent.MapFrom(prop => prop.Ubicacion.Y))
                .ForMember(dto => dto.Longitud, ent => ent.MapFrom(prop => prop.Ubicacion.X));

            CreateMap<Genero, GeneroDto>();

            CreateMap<Pelicula, PeliculaDto>()
                .ForMember(dto => dto.Cines, ent => ent.MapFrom(prop => prop.SalasDeCine.Select(s => s.Cine)))
                .ForMember(dto => dto.Actores, ent => ent.MapFrom(prop => prop.PeliculasActores.Select(pa => pa.Actor)));

            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            CreateMap<CineCreacionDto, Cine>()
                .ForMember(ent => ent.Ubicacion,
                dto => dto.MapFrom(campo =>
                geometryFactory.CreatePoint(new Coordinate(campo.Longitud, campo.Latitud))));

            CreateMap<CineOfertaCreacionDto, CineOferta>();
            CreateMap<SalaDeCineCreacionDto, SalaDeCine>();

            CreateMap<PeliculaCreacionDto, Pelicula>()
                .ForMember(ent => ent.Generos,
                    dto => dto.MapFrom(campo => campo.Generos.Select(id => new Genero() { Identificador = id })))
                .ForMember(ent => ent.SalasDeCine,
                    dto => dto.MapFrom(campo => campo.SalasDeCine.Select(id => new SalaDeCine() { Id = id })));
            CreateMap<PeliculaActorCreacionDto, PeliculaActor>();

            CreateMap<ActorCreacionDto, Actor>().ReverseMap();
        }
    }
}
