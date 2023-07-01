using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Nombre { get; set; }

   

        [Required(ErrorMessage = "Nombre requerido")]
        [MaxLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado requerido")]

        public bool Estado { get; set; }

    }
}
