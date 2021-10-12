using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            NRol Negocios = new NRol();
            List<ERol> Lista = new List<ERol>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        public ActionResult Agregar(ERol Modelo, FormCollection frm)
        {
            try
            {
                if (Modelo.Rol == null && Modelo.Descripcion == null)
                {
                    return View();
                }
                NRol Negocios = new NRol();
                if (frm["Estado"] != null)
                {
                    Modelo.Estado = true;
                }
                int FilasAfectadas = Negocios.Agregar(Modelo);
                if (FilasAfectadas > 0)
                {
                    return Json("success" );
                }
                else
                {
                    return Json("Error" );
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
        public ActionResult Edit(string Id)
        {
            NRol Negocios = new NRol();
            var Rol = Negocios.Mostrar_Detallado(int.Parse(Id));
            return View(Rol);
        }
        //Le indica al metodo que reciba los datos por el metodo post
        [HttpPost]
        ////Evita que se inicie de otro formulario
        //[ValidateAntiForgeryToken]
        //Action result es el tipo de dato que retorna la funcion
        public ActionResult Edit(ERol Model)
        {
            try
            {
                if (Model.Rol == null || Model.Descripcion == null)
                {
                    return View();
                }
                NRol Negocios = new NRol();
                int FilasAfectadas = Negocios.Modificar(Model);
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
                NRol Negocios = new NRol();
                Negocios.Desactivar(int.Parse(id));
                TempData["msg"] = "0";
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}