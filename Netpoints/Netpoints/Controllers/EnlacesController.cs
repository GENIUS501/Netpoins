using Entidades;
using Negocios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class EnlacesController : Controller
    {
        // GET: Enlaces
        public ActionResult Index()
        {
            NEnlaces Negocios = new NEnlaces();
            List<EEnlaces> Lista = new List<EEnlaces>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        public ActionResult Agregar(EEnlaces Modelo)
        {
            try
            {
                NOficinas Oficinas = new NOficinas();
                NRedes Redes = new NRedes();
                NProveedores Proveedores = new NProveedores();
                NEnlaces Enlaces = new NEnlaces();
                ViewBag.ddlOficinas = Oficinas.Mostrar();
                ViewBag.ddlRedes = Redes.Mostrar();
                ViewBag.ddlProveedores = Proveedores.Mostrar(); 
                if (Modelo.IdOficina == 0 && Modelo.IdEnlace == 0)
                {
                    return View();
                }
                int FilasAfectadas = Enlaces.Agregar(Modelo);
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
        public static EOficinas Oficinas(string Id)
        {
            try
            {
                NOficinas Negocios = new NOficinas();
                var Obj = Negocios.Mostrar_Detallado(int.Parse(Id));
                return Obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static ERedes Redes(string Id)
        {
            try
            {
                NRedes Negocios = new NRedes();
                var Obj = Negocios.Mostrar_Detallado(int.Parse(Id));
                return Obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static EProveedores Proveedores(string Id)
        {
            try
            {
                NProveedores Negocios = new NProveedores();
                var Obj = Negocios.Mostrar_Detallado(int.Parse(Id));
                return Obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public ActionResult TraerOficina(string Id)
        {
            try
            {
                NOficinas Negocios = new NOficinas();
                var Obj = Negocios.Mostrar_Detallado(int.Parse(Id));
                if(Obj==null)
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                return Json(Obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
        public ActionResult TraerRedes(string Id)
        {
            try
            {
                NRedes Negocios = new NRedes();
                var Obj = Negocios.Mostrar_Detallado(int.Parse(Id));
                if (Obj == null)
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
                return Json(Obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
    }
}