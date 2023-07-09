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
    public class LineaFacturaConfiguracion : IEntityTypeConfiguration<LineaFactura>
    {
        public void Configure(EntityTypeBuilder<LineaFactura> builder)
        {

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.FacturaId).IsRequired();
            builder.Property(x=> x.ProductoId).IsRequired();
            builder.Property(x=> x.StockAnterior).IsRequired();
            builder.Property(x=> x.Cantidad).IsRequired();
            


            /*Relaciones entre tablas*/


            builder.HasOne(x=> x.Factura).WithMany()
                .HasForeignKey(x=>x.FacturaId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.Producto).WithMany()
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
