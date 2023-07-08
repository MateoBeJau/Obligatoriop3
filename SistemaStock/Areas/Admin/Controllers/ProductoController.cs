using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using SistemaStock.Modelos;
using SistemaStock.Modelos.ViewModels;
using SistemaStock.Utilidades;
using System.Data;

namespace SistemaStock.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = DS.RoleAdmin + "," + DS.RoleInventario)]


    public class ProductoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert (int? id)
        {

            ProductoVM productoVm = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Producto.GetAllDropdownList("Categoria"),
                MarcaLista = _unidadTrabajo.Producto.GetAllDropdownList("Marca")

            };

            if (id == null)
            {
                //Crear producto

                return View (productoVm);
            }
            else
            {
                productoVm.Producto = await _unidadTrabajo.Producto.Get(id.GetValueOrDefault());
                if(productoVm.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVm);

            }
        }


        [HttpPost]
        public async Task<IActionResult> Upsert(ProductoVM productoVM)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if(productoVM.Producto.Id == 0)
                {
                    //Crear Producto
                    string upload = webRootPath + DS.ImagenRut;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productoVM.Producto.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.Producto.Add(productoVM.Producto);
                }
                else
                {
                    //Actualizar
                    var objProducto = await _unidadTrabajo.Producto.GetFirst(p=>p.Id == productoVM.Producto.Id, isTracking: false);
                    if (files.Count>0) //Se carga imagen
                    {
                        string upload = webRootPath+DS.ImagenRut;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //Borrar anterior
                        var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);
                        if(System.IO.File.Exists(anteriorFile)) //Mnejar archivos del sistema
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileSrteam = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileSrteam);
                        }
                        productoVM.Producto.ImagenUrl = fileName + extension;
                    } //Caso que no cambie imagen

                    else
                    {
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _unidadTrabajo.Producto.Update(productoVM.Producto);
                }

                TempData[DS.Exitosa] = "Exitosa";
                await _unidadTrabajo.Save();
                return View("Index");

            }
            //Si falla
            productoVM.CategoriaLista = _unidadTrabajo.Producto.GetAllDropdownList("Categoria");
            productoVM.MarcaLista = _unidadTrabajo.Producto.GetAllDropdownList("Marca");
            return View(productoVM);
        }





        #region
        //Llamadas a codigo js

        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Producto.GetAll(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var productoDb = await _unidadTrabajo.Producto.Get(id);
            if(productoDb == null)
            {
                return Json(new { success = false, message = "Error al borrar producto" });

            }

            //Remover imagen
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRut;
            var anteriorFile = Path.Combine(upload, productoDb.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }

            _unidadTrabajo.Producto.Remove(productoDb);
            await _unidadTrabajo.Save();
            return Json(new { success = true, message = "Producto borrada" });
        }



        [ActionName("validarProducto")]
        public async Task <IActionResult> validarProducto(string numero, int id=0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Producto.GetAll();
            if (id == 0)
            {
                valor = lista.Any(b => b.NumeroProducto.ToLower().Trim() == numero.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NumeroProducto.ToLower().Trim() == numero.ToLower().Trim() && b.Id != id);

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
