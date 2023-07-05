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
    internal class ProductoRepositorio : Repositorio<Producto>, IProductoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public ProductoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if(obj == "Categoria")
            {
                return _db.Categoria.Where(x => x.Estado).Select(a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = a.Id.ToString()
                });
            }
            if (obj == "Marca")
            {
                return _db.Marca.Where(x => x.Estado).Select(a => new SelectListItem
                {
                    Text = a.Nombre,
                    Value = a.Id.ToString()
                });
            }
            return null;
        }

        public void Update(Producto producto)
        {
            var productoDb = _db.Producto.FirstOrDefault(b => b.Id == producto.Id);
            if (productoDb != null)
            {

                if (producto.ImagenUrl != null)
                {
                    producto.ImagenUrl = productoDb.ImagenUrl;
                }
                productoDb.NumeroProducto = producto.NumeroProducto;
                productoDb.Descripcion = producto.Descripcion;
                productoDb.Precio = producto.Precio;
                productoDb.Costo = producto.Costo;
                productoDb.Moneda = producto.Moneda;
                productoDb.CategoriaId = producto.CategoriaId;
                productoDb.MarcaId = producto.MarcaId;
                productoDb.PadreId = producto.PadreId;
                productoDb.Estado = producto.Estado;
                _db.SaveChanges();
            }
        }
    }
}
