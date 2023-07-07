using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos
{
    public class UsuarioAplicacion :IdentityUser
    {
        [Required(ErrorMessage ="Nombre es requerido")]
        [MaxLength(100)]   
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Apellido es requerido")]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Cedula es requerido")]
        
        public int Cedula { get; set; }

        [NotMapped] //Para que no se agrege a la bdd

        public string Role { get; set; }



    }
}
