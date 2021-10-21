using Entidades;
using Negocios;
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
                ViewBag.ddlOficinas = Oficinas.Mostrar();
                ViewBag.ddlRedes = Redes.Mostrar();
                ViewBag.ddlProveedores = Proveedores.Mostrar(); 
                if (Modelo.IdOficina == 0 && Modelo.IdEnlace == 0)
                {
                    return View();
                }
                return View();
                //NUsuario Negocios = new NUsuario();
                //EUsuario Usuario = new EUsuario();
                //Usuario.Contrasena = Helpers.Helper.EncodePassword(string.Concat(Modelo.Usuario.ToString(), Modelo.Contrasena.ToString()));
                //Usuario.Email = Modelo.Email;
                //Usuario.Estado = false;
                //if (Modelo.Estado == "on")
                //{
                //    Usuario.Estado = true;
                //}
                //Usuario.Identificacion = Modelo.Identificacion;
                //Usuario.IdRol = Modelo.IdRol;
                //Usuario.Nombre = Modelo.Nombre;
                //Usuario.Telefono = Modelo.Telefono;
                //Usuario.Usuario = Modelo.Usuario;
                //int FilasAfectadas = Negocios.Agregar(Usuario);
                //if (FilasAfectadas > 0)
                //{
                //    return Json("success");
                //}
                //else
                //{
                //    return Json("Error");
                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
    }
}