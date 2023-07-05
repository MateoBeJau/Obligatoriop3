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
    internal class MarcaRepositorio : Repositorio<Marca>, IMarcaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
           
        public void Update(Marca marca)
        {
            var marcaDB = _db.Marca.FirstOrDefault(b => b.Id == marca.Id);
            if(marcaDB != null)
            {
                marcaDB.Nombre = marca.Nombre;
                marcaDB.Descripcion = marca.Descripcion;
                marcaDB.Estado = marca.Estado;
                _db.SaveChanges();
            }
        }
    }
}
