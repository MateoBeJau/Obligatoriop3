using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using SistemaStock.Modelos;
using SistemaStock.Utilidades;
using System.Data;

namespace SistemaStock.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = DS.RoleAdmin)]

    public class MarcaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public MarcaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert (int? id)
        {
            Marca marca = new Marca();
            //Si id null crea una nueva valija
            if(id == null)
            {
                marca.Estado = true;
                return View(marca);
            }
            //Actualizar valija
            marca = await _unidadTrabajo.Marca.Get(id.GetValueOrDefault()); //por si llega null
            if (marca == null)
            {
                return NotFound(); //Por si se manda un id que no existe
            }
            return View(marca);
     
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //Evitar falsificaciones de solicitudes

        public async Task<IActionResult> Upsert(Marca marca)
        {
            if (ModelState.IsValid)
            {
                if(marca.Id == 0)
                {
                    await _unidadTrabajo.Marca.Add(marca);
                    TempData[DS.Exitosa] = "Marca creada correctamente";
                }
                else
                {
                    _unidadTrabajo.Marca.Update(marca);
                    TempData[DS.Exitosa] = "Categoria actualizada correctamente";

                }
                await _unidadTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Exitosa] = "Error intente nuevamente";
            
            return View(marca);
        }



        #region
        //Llamadas a codigo js

        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Marca.GetAll();
            return Json(new { data = todos });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _unidadTrabajo.Marca.Get(id);
            if(marca == null)
            {
                return Json(new { success = false, message = "Error al borrar marca" });

            }
            _unidadTrabajo.Marca.Remove(marca);
            await _unidadTrabajo.Save();
            return Json(new { success = true, message = "Marca borrada" });
        }



        [ActionName("validarNombre")]
        public async Task <IActionResult> validarNombre(string nombre, int id=0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Marca.GetAll();
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
