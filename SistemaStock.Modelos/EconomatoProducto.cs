using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos
{
    public class EconomatoProducto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EconomatoId { get; set; }

        [ForeignKey("EconomatoId")]
        public Economato Economato { get; set; }

        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
