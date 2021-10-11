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