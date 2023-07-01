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
    internal class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
           
        public void Update(Categoria categoria)
        {
            var categoriaDB = _db.Categoria.FirstOrDefault(b => b.Id == categoria.Id);
            if(categoriaDB != null)
            {
                categoriaDB.Nombre = categoria.Nombre;
                categoriaDB.Descripcion = categoria.Descripcion;
                categoriaDB.Estado = categoria.Estado;
                _db.SaveChanges();
            }
        }
    }
}
