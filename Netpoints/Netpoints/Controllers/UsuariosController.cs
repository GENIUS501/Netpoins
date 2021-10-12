using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {
            NUsuario Negocios = new NUsuario();
            List<EUsuario> Lista = new List<EUsuario>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        public ActionResult Agregar(EUsuario Modelo)
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
                if (Modelo.Identificacion == null && Modelo.Usuario == null)
                {
                    return View();
                }
                NUsuario Negocios = new NUsuario();
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
        #region Traer el nombre del Rol
        public static string NombreRol(string id)
        {
            try
            {
                NUsuario Negocios = new NUsuario();
                return Negocios.Nombre_Rol(int.Parse(id));
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}