using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using SistemaStock.Modelos;
using SistemaStock.Utilidades;

namespace SistemaStock.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles =DS.RoleAdmin)]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert (int? id)
        {
            Categoria categoria = new Categoria();
            //Si id null crea una nueva valija
            if(id == null)
            {
                categoria.Estado = true;
                return View(categoria);
            }
            //Actualizar valija
            categoria = await _unidadTrabajo.Categoria.Get(id.GetValueOrDefault()); //por si llega null
            if (categoria == null)
            {
                return NotFound(); //Por si se manda un id que no existe
            }
            return View(categoria);
     
        }


        [HttpPost]
        [ValidateAntiForgeryToken] //Evitar falsificaciones de solicitudes

        public async Task<IActionResult> Upsert(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if(categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Add(categoria);
                    TempData[DS.Exitosa] = "Categoria creada correctamente";
                }
                else
                {
                    _unidadTrabajo.Categoria.Update(categoria);
                    TempData[DS.Exitosa] = "Categoria actualizada correctamente";

                }
                await _unidadTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Exitosa] = "Error intente nuevamente";
            
            return View(categoria);
        }



        #region
        //Llamadas a codigo js

        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.GetAll();
            return Json(new { data = todos });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _unidadTrabajo.Categoria.Get(id);
            if(categoria == null)
            {
                return Json(new { success = false, message = "Error al borrar categoria" });

            }
            _unidadTrabajo.Categoria.Remove(categoria);
            await _unidadTrabajo.Save();
            return Json(new { success = true, message = "Categoria borrada" });
        }



        [ActionName("validarNombre")]
        public async Task <IActionResult> validarNombre(string nombre, int id=0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Categoria.GetAll();
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
