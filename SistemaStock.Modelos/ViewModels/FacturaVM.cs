using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.Modelos.ViewModels
{
    public class FacturaVM
    {
        public Factura Factura { get; set; }
        public LineaFactura LineaFactura { get; set; }
        public IEnumerable<LineaFactura> LineaFacturas { get; set; }

        public IEnumerable<SelectListItem> EconomatoLista { get; set; }
    }
}
