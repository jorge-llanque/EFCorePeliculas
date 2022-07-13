using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class CineOfertaConfig : IEntityTypeConfiguration<CineOferta>
    {
        public void Configure(EntityTypeBuilder<CineOferta> builder)
        {
            builder.Property(prop => prop.PorcentajeDescuento)
                .HasPrecision(precision: 5, scale: 2);
            builder.Property(prop => prop.FechaInicio)
                .HasColumnType("date");
            builder.Property(prop => prop.FechaFin)
                .HasColumnType("date");
        }
    }
}
