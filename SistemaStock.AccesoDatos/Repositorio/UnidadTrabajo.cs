using SistemaStock.AccesoDatos.Data;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {

        private readonly ApplicationDbContext _db;

        public IEconomatoRepositorio Economato { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Economato = new EconomatoRepositorio(_db);
        }


        public void Dispose()
        {
            _db.Dispose(); //elimina rlo q tenemos en memoria

        }

        public async Task Save()
        {
           await _db.SaveChangesAsync();
        }
    }
}
