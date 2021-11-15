using Entidades;
using Negocios;
using Netpoints.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class RedesController : Controller
    {
        [AuthorizeUser(idmodulo: 4)]
        public ActionResult Index()
        {
            NRedes Negocios = new NRedes();
            List<ERedes> Lista = new List<ERedes>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        [AuthorizeUserPermises(accion: "A", idmodulo: 4)]
        public ActionResult Agregar(ERedes Modelo)
        {
            try
            {
                if (Modelo.Linea == null && Modelo.Gateway == null)
                {
                    return View();
                }
                NRedes Negocios = new NRedes();
                EUsuario User = (EUsuario)Session["User"];
                int FilasAfectadas = Negocios.Agregar(Modelo,User.IdUsuario);
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
        [AuthorizeUserPermises(accion: "E", idmodulo: 4)]
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            NRedes Negocios = new NRedes();
            var Modelo = Negocios.Mostrar_Detallado(int.Parse(Id));
            return View(Modelo);
        }
        [AuthorizeUserPermises(accion: "E", idmodulo: 4)]
        [HttpPost]
        public ActionResult Edit(ERedes Modelo)
        {
            try
            {
                NRedes Negocios = new NRedes();
                EUsuario User = (EUsuario)Session["User"];
                int FilasAfectadas = Negocios.Modificar(Modelo,User.IdUsuario);
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
        [AuthorizeUserPermises(accion: "D", idmodulo: 4)]
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                NRedes Negocios = new NRedes();
                EUsuario User = (EUsuario)Session["User"];
                int FilasAfectadas = Negocios.Eliminar(int.Parse(id),User.IdUsuario);
                if (FilasAfectadas > 0)
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