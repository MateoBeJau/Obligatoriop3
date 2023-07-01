using Microsoft.AspNetCore.Mvc;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using SistemaStock.Modelos;
using SistemaStock.Utilidades;

namespace SistemaStock.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class EconomatoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public EconomatoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert (int? id)
        {
            Economato economato = new Economato();
            //Si id null crea una nueva valija
            if(id == null)
            {
                economato.Estado = true;
                return View(economato);
            }
             //Actualizar valija
             economato = await _unidadTrabajo.Economato.Get(id.GetValueOrDefault()); //por si llega null
            if (economato == null)
            {
                return NotFound(); //Por si se manda un id que no existe
            }
            return View(economato);
     
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //Evitar falsificaciones de solicitudes

        public async Task<IActionResult> Upsert(Economato economato)
        {
            if (ModelState.IsValid)
            {
                if(economato.Id == 0)
                {
                    await _unidadTrabajo.Economato.Add(economato);
                    TempData[DS.Exitosa] = "Valija creada correctamente";
                }
                else
                {
                    _unidadTrabajo.Economato.Update(economato);
                    TempData[DS.Exitosa] = "Valija actualizada correctamente";

                }
                await _unidadTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Exitosa] = "Error intente nuevamente";
            
            return View(economato);
        }



        #region
        //Llamadas a codigo js

        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Economato.GetAll();
            return Json(new { data = todos });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var economato = await _unidadTrabajo.Economato.Get(id);
            if(economato == null)
            {
                return Json(new { success = false, message = "Error al borrar valija" });

            }
            _unidadTrabajo.Economato.Remove(economato);
            await _unidadTrabajo.Save();
            return Json(new { success = true, message = "Valija borrada" });
        }



        [ActionName("validarNombre")]
        public async Task <IActionResult> validarNombre(string nombre, int id=0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Economato.GetAll();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);

            }
            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { success = false });
        }

        #endregion
    }
}
