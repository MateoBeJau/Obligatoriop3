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
    public class EconomatoProductoConfiguracion : IEntityTypeConfiguration<EconomatoProducto>
    {
        public void Configure(EntityTypeBuilder<EconomatoProducto> builder)
        {

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.EconomatoId).IsRequired();
            builder.Property(x=>x.ProductoId).IsRequired();
            builder.Property(x=>x.Cantidad).IsRequired();


            /*Relaciones entre tablas*/


            builder.HasOne(x=> x.Economato).WithMany()
                .HasForeignKey(x=>x.EconomatoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Producto).WithMany()
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
