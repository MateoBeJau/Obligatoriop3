using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaStock.AccesoDatos.Data;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using SistemaStock.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio
{
    internal class EconomatoProductoRepositorio : Repositorio<EconomatoProducto>, IEconomatoProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EconomatoProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(EconomatoProducto economatoProducto)
        {
            var economatoProductoDb = _db.EconomatoProducto.FirstOrDefault(b => b.Id == economatoProducto.Id);
            if (economatoProductoDb != null)
            {

                economatoProductoDb.Cantidad = economatoProducto.Cantidad;

                _db.SaveChanges();
            }
        }
    }
}



//public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
//{
//    if(obj == "Categoria")
//    {
//        return _db.Categoria.Where(x => x.Estado).Select(a => new SelectListItem
//        {
//            Text = a.Nombre,
//            Value = a.Id.ToString()
//        });
//    }
//    if (obj == "Marca")
//    {
//        return _db.Marca.Where(x => x.Estado).Select(a => new SelectListItem
//        {
//            Text = a.Nombre,
//            Value = a.Id.ToString()
//        });
//    }
//    return null;
//}
