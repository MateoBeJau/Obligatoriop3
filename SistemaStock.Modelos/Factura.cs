using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos
{
    public class Factura
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string UsuarioAplicacionId { get; set; } //Para ver quien cargo la factura o vendio

        [ForeignKey("UsuarioAplicacionId")]
        public UsuarioAplicacion UsuarioAplicacion { get; set; }

        [Required]
        public DateTime FechaFactura { get; set; }

        [Required]
        public int EconomatoId { get; set; }

        [ForeignKey("EconomatoId")]
        public Economato Economato { get; set; }

        [Required]
        public bool Estado { get; set; } //Tipo factura



    }
}
