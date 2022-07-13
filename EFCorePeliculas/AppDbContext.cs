using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EFCorePeliculas.Entidades;
using EFCorePeliculas.Entidades.Seeding;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculas
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Cargar datos
            SeedingModuloConsulta.Seed(modelBuilder);

        }

        public DbSet<Genero> generos { get; set; }
        public DbSet<Actor> actores { get; set; }
        public DbSet<Cine> cines { get; set; }
        public DbSet<Pelicula> peliculas { get; set; }
        public DbSet<CineOferta> cineOfertas { get; set; }
        public DbSet<SalaDeCine> salasDeCine { get; set; }
    }
}
