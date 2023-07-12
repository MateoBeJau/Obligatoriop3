using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using SistemaStock.Modelos;
using SistemaStock.Modelos.ErrorViewModels;
using SistemaStock.Modelos.ViewModels;

using System.Diagnostics;

namespace SistemaStock.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;


        public HomeController(ILogger<HomeController> logger,IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;

        }

        //Ver como arreglar
        public  IActionResult Index()
        {


            return View();
        }
        public async Task<IActionResult> Privacy()
        {
            IEnumerable<Producto> productoLista =  await _unidadTrabajo.Producto.GetAll();
            Api cotizacion = new Api();
            string resultado = cotizacion.GetCotizacion("2754ab043aeb1bd0702bf8d166baf836");
            var retornoCotizacion = JsonConvert.DeserializeObject<Cotizacion>(resultado).Quotes["USDUYU"];
            ViewData["Cotizacion"] = retornoCotizacion;
            return View(productoLista);
        }

        public async Task<IActionResult> Detalle(int id)
        {


            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}