using Entidades;
using Negocios;
using Netpoints.Filters;
using Netpoints.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        [AuthorizeUser(idmodulo: 1)]
        public ActionResult Index()
        {
            NUsuario Negocios = new NUsuario();
            List<EUsuario> Lista = new List<EUsuario>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        [AuthorizeUserPermises(accion: "A", idmodulo: 1)]
        public ActionResult Agregar(EUsuarioViewModel Modelo)
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
                if (Modelo.Identificacion == null || Modelo.Usuario == null)
                {
                    return View();
                }
                NUsuario Negocios = new NUsuario();
                EUsuario Usuario = new EUsuario();
                Usuario.Contrasena = Helpers.Helper.EncodePassword(string.Concat(Modelo.Usuario.ToString(), Modelo.Contrasena.ToString()));
                Usuario.Email = Modelo.Email;
                Usuario.Estado = false;
                if (Modelo.Estado == "on")
                {
                    Usuario.Estado = true;
                }
                Usuario.Identificacion = Modelo.Identificacion;
                Usuario.IdRol = Modelo.IdRol;
                Usuario.Nombre = Modelo.Nombre;
                Usuario.Telefono = Modelo.Telefono;
                Usuario.Usuario = Modelo.Identificacion;
                EUsuario User = (EUsuario)Session["User"];
                int FilasAfectadas = Negocios.Agregar(Usuario,User.IdUsuario);
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
        [AuthorizeUserPermises(accion: "E", idmodulo: 1)]
        public ActionResult Edit(string Id)
        {
            #region Llenar drop down Roles
            //llena la lista que a su vez es una entidad
            NUsuario negocios = new NUsuario();
            var lista1 = negocios.LlenarRoles();
            //Asignar y convertir los valores a items
            var items = new SelectList(lista1, "IdRol", "Descripcion");
            ViewBag.Roles = items;
            #endregion
            NUsuario Negocios = new NUsuario();
            var Usuario = Negocios.Mostrar_Detallado(int.Parse(Id));
            EUsuarioViewModel UsuarioV = new EUsuarioViewModel();
            UsuarioV.IdRol = Usuario.IdRol;
            UsuarioV.Contrasena = Usuario.Contrasena;
            UsuarioV.Email = Usuario.Email;
            UsuarioV.Estado = Usuario.Estado.ToString();
            UsuarioV.Identificacion = Usuario.Identificacion;
            UsuarioV.IdUsuario = Usuario.IdUsuario;
            UsuarioV.Nombre = Usuario.Nombre;
            UsuarioV.Telefono = Usuario.Telefono;
            UsuarioV.Usuario = Usuario.Usuario;
            return View(UsuarioV);
        }
        [AuthorizeUserPermises(accion: "E", idmodulo: 1)]
        [HttpPost]
        public ActionResult Edit(EUsuarioViewModel Modelo)
        {
            try
            {
                NUsuario Negocios = new NUsuario();
                EUsuario Usuario = new EUsuario();
                if (Modelo.Contrasena == "Contra001")
                {
                    Usuario.Contrasena = Modelo.Contrasena;
                }
                else
                {
                    Usuario.Contrasena = Helpers.Helper.EncodePassword(string.Concat(Modelo.Usuario.ToString(), Modelo.Contrasena.ToString()));
                }
                Usuario.Email = Modelo.Email;
                Usuario.Estado = false;
                if (Modelo.Estado == "on")
                {
                    Usuario.Estado = true;
                }
                Usuario.Identificacion = Modelo.Identificacion;
                Usuario.IdRol = Modelo.IdRol;
                Usuario.Nombre = Modelo.Nombre;
                Usuario.Telefono = Modelo.Telefono;
                Usuario.Usuario = Modelo.Usuario;
                Usuario.IdUsuario = Modelo.IdUsuario;
                EUsuario User = (EUsuario)Session["User"];
                int FilasAfectadas = Negocios.Modificar(Usuario,User.IdUsuario);
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
        [AuthorizeUserPermises(accion: "D", idmodulo: 1)]
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                NUsuario Negocios = new NUsuario();
                EUsuario User = (EUsuario)Session["User"];
                Negocios.Desactivar(int.Parse(id),User.IdUsuario);
                TempData["msg"] = "0";
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
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
        public async Task<ActionResult> Verificar(string id)
        {
            try
            {
                NUsuario Negocios = new NUsuario();
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
        #endregion
    }
}