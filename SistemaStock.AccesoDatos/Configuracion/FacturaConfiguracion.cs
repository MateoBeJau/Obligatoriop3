using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaStock.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Configuracion
{
    public class FacturaConfiguracion : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.EconomatoId).IsRequired();
            builder.Property(x=> x.UsuarioAplicacionId).IsRequired();
            builder.Property(x=> x.FechaFactura).IsRequired();
            builder.Property(x=> x.Estado).IsRequired();
            


            /*Relaciones entre tablas*/


            builder.HasOne(x=> x.Economato).WithMany()
                .HasForeignKey(x=>x.EconomatoId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.UsuarioAplicacion).WithMany()
                .HasForeignKey(x => x.UsuarioAplicacionId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
