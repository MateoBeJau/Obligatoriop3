using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Numero serie de Producto requerido")]
        [MaxLength(20)]
        public string NumeroProducto { get; set; }
        [Required(ErrorMessage = "Descripcion de Producto requerido")]
        [MaxLength(50)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Precio de Producto requerido")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Costo de Producto requerido")]

        public double Costo { get; set; }

        public string ImagenUrl { get; set; }
        public string ImagenUrlDos { get; set; }
        public string ImagenUrlTres { get; set; }

        [Required(ErrorMessage = "Moneda de Producto requerido")]

        public bool Moneda { get; set; }

        [Required(ErrorMessage = "Categoria de Producto requerido")]

        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }


        [Required(ErrorMessage = "Marca de Producto requerido")]

        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        public int? PadreId { get; set; } //Para no tener porblema con la realacion

        public virtual Producto Padre { get; set; }

        [Required(ErrorMessage = "Estado requerido")]

        public bool Estado { get; set; }

    }
}
