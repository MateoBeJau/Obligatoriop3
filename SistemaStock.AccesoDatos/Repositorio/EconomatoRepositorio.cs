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
    internal class EconomatoRepositorio : Repositorio<Economato>, IEconomatoRepositorio
    {
        private readonly ApplicationDbContext _db;

        public EconomatoRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
           
        public void Update(Economato economato)
        {
            var economatoDB = _db.Economato.FirstOrDefault(b => b.Id == economato.Id);
            if(economatoDB != null)
            {
                economatoDB.Nombre = economato.Nombre;
                economatoDB.Descripcion = economato.Descripcion;
                economatoDB.Estado = economato.Estado;
                _db.SaveChanges();
            }
        }
    }
}
