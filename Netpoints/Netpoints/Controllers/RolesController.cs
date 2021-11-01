using Entidades;
using Negocios;
using Netpoints.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult Agregar(ERolViewModel Modelo)
        {
            try
            {
                if (Modelo.Rol == null && Modelo.Descripcion == null)
                {
                    return View();
                }
                NRol Negocios = new NRol();
                ERol Rol = new ERol();
                Rol.Rol = Modelo.Rol;
                Rol.Descripcion = Modelo.Descripcion;
                Rol.Estado = false;
                if(Modelo.Estado=="on")
                {
                    Rol.Estado = true;
                }
                int FilasAfectadas = Negocios.Agregar(Rol);
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
            ERolViewModel RolV = new ERolViewModel();
            RolV.IdRol = Rol.IdRol;
            RolV.Rol = Rol.Rol;
            RolV.Descripcion = Rol.Descripcion;
            RolV.Estado = Rol.Estado.ToString();
            return View(RolV);
        }
        //Le indica al metodo que reciba los datos por el metodo post
        [HttpPost]
        ////Evita que se inicie de otro formulario
        //[ValidateAntiForgeryToken]
        //Action result es el tipo de dato que retorna la funcion
        public ActionResult Edit(ERolViewModel Model)
        {
            try
            {
                if (Model.Rol == null || Model.Descripcion == null)
                {
                    return View();
                }
                NRol Negocios = new NRol();
                ERol Rol = new ERol();
                Rol.IdRol = Model.IdRol;
                Rol.Rol = Model.Rol;
                Rol.Descripcion = Model.Descripcion;
                Rol.Estado = false;
                if (Model.Estado == "False")
                {
                    Rol.Estado = true;
                }
                int FilasAfectadas = Negocios.Modificar(Rol);
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
        [HttpGet]
        public async Task<ActionResult> Verificar(string id)
        {
            try
            {
                NRol Negocios = new NRol();
                bool NoExiste = await Negocios.Verificar(id);
                if (NoExiste)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}