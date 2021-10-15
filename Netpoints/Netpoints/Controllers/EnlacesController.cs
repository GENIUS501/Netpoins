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
                #region Llenar drop down Roles
                //llena la lista que a su vez es una entidad
                NUsuario negocios = new NUsuario();
                var lista1 = negocios.LlenarRoles();
                //Asignar y convertir los valores a items
                var items = new SelectList(lista1, "IdRol", "Descripcion");
                //List<SelectListItem> items = lista1.ConvertAll(d =>
                //{
                //    return new SelectListItem()
                //    {
                //        Text = d.Descripcion,
                //        Value = d.IdRol.ToString(),
                //        Selected = false
                //    };
                //}
                //);
                //enviar items a la vista
                ViewBag.Roles = items;
                #endregion
                if (Modelo.IdOficina == null && Modelo.IdEnlace == null)
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