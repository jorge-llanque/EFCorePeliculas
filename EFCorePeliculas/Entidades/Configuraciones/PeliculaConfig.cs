using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            builder.Property(prop => prop.Titulo)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(prop => prop.FechaEstreno)
                .HasColumnType("date");
            builder.Property(prop => prop.PosterURL)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
