using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        public ActionResult Index()
        {
            NProveedores Negocios = new NProveedores();
            List<EProveedores> Lista = new List<EProveedores>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        public ActionResult Agregar(EProveedores Modelo)
        {
            try
            {
                if (Modelo.NombreEmpresa == null && Modelo.Telefono == null)
                {
                    return View();
                }
                NProveedores Negocios = new NProveedores();
                int FilasAfectadas = Negocios.Agregar(Modelo);
                if (FilasAfectadas > 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            NProveedores Negocios = new NProveedores();
            var Modelo = Negocios.Mostrar_Detallado(int.Parse(Id));
            return View(Modelo);
        }

        [HttpPost]
        public ActionResult Edit(EProveedores Modelo)
        {
            try
            {
                NProveedores Negocios = new NProveedores();
                int FilasAfectadas = Negocios.Modificar(Modelo);
                if (FilasAfectadas > 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                NProveedores Negocios = new NProveedores();
                int FilasAfectadas=Negocios.Eliminar(int.Parse(id));
                if(FilasAfectadas>0)
                {
                    TempData["msg"] = "0";
                }
                else
                {
                    TempData["msg"] = "-1";
                }
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Redirect("Index");
            }
        }
    }
}