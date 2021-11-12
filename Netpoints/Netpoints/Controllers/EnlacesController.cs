using Entidades;
using Negocios;
using Netpoints.Filters;
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
        //Autorizar acceso al modulo
        [AuthorizeUser(idmodulo: 11)]
        public ActionResult Index()
        {
            NEnlaces Negocios = new NEnlaces();
            List<EEnlaces> Lista = new List<EEnlaces>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        // Autorizar acceso a la accion
        [AuthorizeUserPermises(accion: "A", idmodulo: 11)]
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
        // Autorizar acceso a la accion
        [AuthorizeUserPermises(accion: "E", idmodulo: 11)]
        public ActionResult Edit(string Id)
        {
            #region Llenar drop down oficinas
            NOficinas Negociosofi = new NOficinas();
            var lista1 = Negociosofi.Mostrar();
            var items = new SelectList(lista1, "IdOficina", "NombreOficina");
            ViewBag.ddloficinas = items;
            #endregion
            #region Llenar drop down redes
            NRedes Negociosredes = new NRedes();
            var listaredes = Negociosredes.Mostrar();
            var itemsredes = new SelectList(listaredes, "IdRed", "Linea");
            ViewBag.ddlredes = itemsredes;
            #endregion
            #region Llenar drop down proveedores
            NProveedores Negociosprov = new NProveedores();
            var listaprov = Negociosprov.Mostrar();
            var itemsprov = new SelectList(listaprov, "IdProveedor", "NombreEmpresa");
            ViewBag.ddlproveedores = itemsprov;
            #endregion
            NEnlaces Negocios = new NEnlaces();
            var Modelo = Negocios.Mostrar_Detallado(int.Parse(Id));
            ViewBag.Oficinas = Oficinas(Modelo.IdOficina.ToString());
            ViewBag.Redes = Redes(Modelo.IdRed.ToString());
            ViewBag.Proveedores = Proveedores(Modelo.IdProveedor.ToString());
            return View(Modelo);
        }
        // Autorizar acceso a la accion
        [AuthorizeUserPermises(accion: "E", idmodulo: 11)]
        [HttpPost]
        public ActionResult Edit(EEnlaces Modelo)
        {
            try
            {
                NEnlaces Negocios = new NEnlaces();
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
        // Autorizar acceso a la accion
        [AuthorizeUserPermises(accion: "D", idmodulo: 11)]
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                NEnlaces Negocios = new NEnlaces();
                int FilasAfectadas = Negocios.Eliminar(int.Parse(id));
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