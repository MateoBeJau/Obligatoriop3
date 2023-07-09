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
    internal class FacturaRepositorio : Repositorio<Factura>, IFacturaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public FacturaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Factura factura)
        {
            var facturaDB = _db.Factura.FirstOrDefault(b => b.Id == factura.Id);
            if (facturaDB != null)
            {

                facturaDB.EconomatoId = factura.EconomatoId;
                facturaDB.FechaFactura = factura.FechaFactura;
                facturaDB.Estado = factura.Estado;

                _db.SaveChanges();
            }
        }
    }
}



