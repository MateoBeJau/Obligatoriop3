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
    public class ProductoConfiguracion : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.NumeroProducto).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Moneda).IsRequired();
            builder.Property(x => x.Precio).IsRequired();
            builder.Property(x => x.Costo).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();
            builder.Property(x => x.MarcaId).IsRequired();
            builder.Property(x => x.ImagenUrl).IsRequired(false);
            builder.Property(x => x.ImagenUrlDos).IsRequired(false);
            builder.Property(x => x.ImagenUrlTres).IsRequired(false);
            builder.Property(x => x.PadreId).IsRequired(false);
            builder.Property(x=>x.Estado).IsRequired();



            /*Relaciones entre tablas*/

            builder.HasOne(x => x.Categoria).WithMany()
                    .HasForeignKey(x => x.CategoriaId)
                    .OnDelete(DeleteBehavior.NoAction); //Cuando se borra una categoria y esta relacionado a un producto, para evitar errores

            builder.HasOne(x => x.Marca).WithMany()
                    .HasForeignKey(x => x.MarcaId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Padre).WithMany().HasForeignKey(x => x.PadreId).OnDelete(DeleteBehavior.NoAction);



        }
    }
}
