using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaStock.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio.IRepositorio
{
    public interface IEconomatoProductoRepositorio :IRepositorio <EconomatoProducto>
    {
        void Update(EconomatoProducto economatoProducto);

        //IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
