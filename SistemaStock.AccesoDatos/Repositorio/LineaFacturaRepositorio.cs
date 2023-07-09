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
    internal class LineaFacturaRepositorio : Repositorio<LineaFactura>, ILineaFacturaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public LineaFacturaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(LineaFactura lineaFactura)
        {
            var lineaFacturaDB = _db.LineaFactura.FirstOrDefault(b => b.Id == lineaFactura.Id);
            if (lineaFacturaDB != null)
            {
                lineaFacturaDB.StockAnterior = lineaFactura.StockAnterior;
                lineaFacturaDB.Cantidad = lineaFactura.Cantidad;
    
                _db.SaveChanges();
            }
        }
    }
}



