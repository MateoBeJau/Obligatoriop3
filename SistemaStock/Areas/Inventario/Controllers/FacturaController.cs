using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using SistemaStock.Modelos;
using SistemaStock.Modelos.ViewModels;
using SistemaStock.Utilidades;
using System.Security.Claims;

namespace SistemaStock.Areas.Inventario.Controllers
{

    [Area("Inventario")]
    [Authorize(Roles = DS.RoleAdmin + "," + DS.RoleInventario)]
    public class FacturaController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;

        [BindProperty]
        public FacturaVM facturaVM { get; set; }
        public FacturaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FacturaVenta()
        {
            facturaVM = new FacturaVM()
            {
                Factura = new Modelos.Factura(),
                EconomatoLista = _unidadTrabajo.Factura.GetAllDropdownList("Economato"),

            };

            facturaVM.Factura.Estado = false;
            //Obtener el usuario que esta conectado en ese momento
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            facturaVM.Factura.UsuarioAplicacionId = claim.Value;
            facturaVM.Factura.FechaFactura = DateTime.Now;

            return View(facturaVM);
        }

        public IActionResult NuevaFactura()
        {
            facturaVM = new FacturaVM()
            {
                Factura = new Modelos.Factura(),
                EconomatoLista = _unidadTrabajo.Factura.GetAllDropdownList("Economato"),

            };

            facturaVM.Factura.Estado = false;
            //Obtener el usuario que esta conectado en ese momento
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            facturaVM.Factura.UsuarioAplicacionId = claim.Value;
            facturaVM.Factura.FechaFactura = DateTime.Now;

            return View(facturaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Cabezera factura Compra
        public async Task<IActionResult> NuevaFactura (FacturaVM facturaVM)
        {
            if(ModelState.IsValid)
            {
                facturaVM.Factura.FechaFactura = DateTime.Now; //La factura se carga con la fecha dle dia q se cargo
                await _unidadTrabajo.Factura.Add(facturaVM.Factura);
                await _unidadTrabajo.Save();
                return RedirectToAction("DetalleFactura", new {id= facturaVM.Factura.Id});
            }
            facturaVM.EconomatoLista = _unidadTrabajo.Factura.GetAllDropdownList("Economato");
            return View(facturaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Cabezera factura venta
        public async Task<IActionResult> FacturaVenta(FacturaVM facturaVM)
        {
            if (ModelState.IsValid)
            {
                facturaVM.Factura.FechaFactura = DateTime.Now; //La factura se carga con la fecha dle dia q se cargo
                await _unidadTrabajo.Factura.Add(facturaVM.Factura);
                await _unidadTrabajo.Save();
                return RedirectToAction("DetalleFacturaVenta", new { id = facturaVM.Factura.Id });
            }
            facturaVM.EconomatoLista = _unidadTrabajo.Factura.GetAllDropdownList("Economato");
            return View(facturaVM);
        }

        public async Task<IActionResult> DetalleFactura (int id)
        {
            Api cotizacion = new Api();
            string resultado = cotizacion.GetCotizacion("2754ab043aeb1bd0702bf8d166baf836");
            var retornoCotizacion = JsonConvert.DeserializeObject<Cotizacion>(resultado).Quotes["USDUYU"];
            ViewData["Cotizacion"] = retornoCotizacion;

            facturaVM = new FacturaVM();
            facturaVM.Factura = await _unidadTrabajo.Factura.GetFirst(i => i.Id == id, incluirPropiedades: "Economato");
            facturaVM.LineaFacturas = await _unidadTrabajo.LineaFactura.GetAll(d => d.FacturaId == id, 
                incluirPropiedades: "Producto,Producto.Marca");

            return View(facturaVM);
        }

        public async Task<IActionResult> DetalleFacturaVenta(int id)
        {
            Api cotizacion = new Api();
            string resultado = cotizacion.GetCotizacion("2754ab043aeb1bd0702bf8d166baf836");
            var retornoCotizacion = JsonConvert.DeserializeObject<Cotizacion>(resultado).Quotes["USDUYU"];
            ViewData["Cotizacion"] = retornoCotizacion;

            facturaVM = new FacturaVM();
            facturaVM.Factura = await _unidadTrabajo.Factura.GetFirst(i => i.Id == id, incluirPropiedades: "Economato");
            facturaVM.LineaFacturas = await _unidadTrabajo.LineaFactura.GetAll(d => d.FacturaId == id,
                incluirPropiedades: "Producto,Producto.Marca");

            return View(facturaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Factura compra
        public async Task<IActionResult> DetalleFactura(int FacturaId, int productoId, int cantidadId)
        {


            facturaVM = new FacturaVM();
            facturaVM.Factura = await _unidadTrabajo.Factura.GetFirst(f => f.Id == FacturaId);
            var economatoProducto = await _unidadTrabajo.EconomatoProducto.GetFirst(e => e.ProductoId == productoId &&
                                                                                    e.EconomatoId == facturaVM.Factura.EconomatoId);

            var detalle = await _unidadTrabajo.LineaFactura.GetFirst(d => d.FacturaId == FacturaId &&
                                                                        d.ProductoId == productoId);

            if (detalle == null)
            {
                facturaVM.LineaFactura = new Modelos.LineaFactura();
                facturaVM.LineaFactura.ProductoId = productoId;
                facturaVM.LineaFactura.FacturaId = FacturaId;
                if(economatoProducto != null)
                {
                    facturaVM.LineaFactura.StockAnterior = economatoProducto.Cantidad;
                }
                else
                {
                    facturaVM.LineaFactura.StockAnterior = 0;
                }
                facturaVM.LineaFactura.Cantidad = cantidadId;
                await _unidadTrabajo.LineaFactura.Add(facturaVM.LineaFactura);
                await _unidadTrabajo.Save();
            }
            else
            {
                detalle.Cantidad += cantidadId;
                await _unidadTrabajo.Save();
            }
            return RedirectToAction("DetalleFactura");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Factura venta
        public async Task<IActionResult> DetalleFacturaVenta(int FacturaId, int productoId, int cantidadId)
        {


            facturaVM = new FacturaVM();
            facturaVM.Factura = await _unidadTrabajo.Factura.GetFirst(f => f.Id == FacturaId);
            var economatoProducto = await _unidadTrabajo.EconomatoProducto.GetFirst(e => e.ProductoId == productoId &&
                                                                                    e.EconomatoId == facturaVM.Factura.EconomatoId);

            var detalle = await _unidadTrabajo.LineaFactura.GetFirst(d => d.FacturaId == FacturaId &&
                                                                        d.ProductoId == productoId);

            if (detalle == null)
            {
                facturaVM.LineaFactura = new Modelos.LineaFactura();
                facturaVM.LineaFactura.ProductoId = productoId;
                facturaVM.LineaFactura.FacturaId = FacturaId;
                if (economatoProducto != null)
                {
                    facturaVM.LineaFactura.StockAnterior = economatoProducto.Cantidad;
                }
                else
                {
                    facturaVM.LineaFactura.StockAnterior = 0;
                }
                facturaVM.LineaFactura.Cantidad = cantidadId;
                await _unidadTrabajo.LineaFactura.Add(facturaVM.LineaFactura);
                await _unidadTrabajo.Save();
            }
            else
            {
                detalle.Cantidad += cantidadId;
                await _unidadTrabajo.Save();
            }
            return RedirectToAction("DetalleFacturaVenta");
        }


        public async Task<IActionResult> GenerarStock(int id)
        {
            var factura = await _unidadTrabajo.Factura.Get(id);
            var detalleLista = await _unidadTrabajo.LineaFactura.GetAll(d=>d.FacturaId == id);

            foreach (var item in detalleLista)
            {
               
                 var economatoProducto = await _unidadTrabajo.EconomatoProducto.GetFirst(e=> e.ProductoId== item.ProductoId &&
                                                                                    e.EconomatoId == factura.EconomatoId);

                if(economatoProducto !=null) //Si existe el producto hay q actualizar las cantidades
                {
                    economatoProducto.Cantidad += item.Cantidad;

                }
                else
                {
                    economatoProducto = new EconomatoProducto();
                    economatoProducto.EconomatoId = factura.EconomatoId;
                    economatoProducto.ProductoId = item.ProductoId;
                    economatoProducto.Cantidad = item.Cantidad;
                    await _unidadTrabajo.EconomatoProducto.Add(economatoProducto);

                }
            }
            await _unidadTrabajo.Save();

            TempData[DS.Exitosa] = "Factura cargada con exito";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GenerarFacturaVenta(int id)
        {
            var factura = await _unidadTrabajo.Factura.Get(id);
            var detalleLista = await _unidadTrabajo.LineaFactura.GetAll(d => d.FacturaId == id);

            foreach (var item in detalleLista)
            {

                var economatoProducto = await _unidadTrabajo.EconomatoProducto.GetFirst(e => e.ProductoId == item.ProductoId &&
                                                                                   e.EconomatoId == factura.EconomatoId);

                if (economatoProducto != null) //Si existe el producto hay q actualizar las cantidades
                {
                    economatoProducto.Cantidad -= item.Cantidad;

                }
                else
                {
                    economatoProducto = new EconomatoProducto();
                    economatoProducto.EconomatoId = factura.EconomatoId;
                    economatoProducto.ProductoId = item.ProductoId;
                    economatoProducto.Cantidad = item.Cantidad;
                    await _unidadTrabajo.EconomatoProducto.Add(economatoProducto);

                }
            }
            await _unidadTrabajo.Save();

            TempData[DS.Exitosa] = "Factura cargada con exito";
            return RedirectToAction("Index");
        }


        #region API

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.EconomatoProducto.GetAll(incluirPropiedades: "Economato,Producto");
            return Json(new { data= todos }); //llamada por js
        }


        [HttpGet]

        public async Task<IActionResult> BuscarProducto(string term)
        {
            if (!string.IsNullOrEmpty(term)) 
            {
                var listaProductos = await _unidadTrabajo.Producto.GetAll(p => p.Estado == true);
                var data =listaProductos.Where(x=> x.NumeroProducto.Contains(term, StringComparison.OrdinalIgnoreCase)||
                                                    x.Descripcion.Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();

                return Ok(data);
            }
            return Ok();
        }



        #endregion
    }
}
