using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaStock.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio.IRepositorio
{
    public interface IFacturaRepositorio :IRepositorio <Factura>
    {
        void Update(Factura factura);

        //IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
