using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos
{
    public class LineaFactura
    {

        [Key] 
        public int Id { get; set; }
        [Required]
        public int FacturaId { get; set; }

        [ForeignKey("FacturaId")]
        public Factura Factura { get; set; }

        [Required]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [Required]

        public int StockAnterior { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
