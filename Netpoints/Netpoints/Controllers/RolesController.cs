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
        public ActionResult Agregar(ERol Modelo)
        {
            try
            {
                if (Modelo.Rol == null && Modelo.Descripcion == null)
                {
                    return View();
                }
                NRol Negocios = new NRol();
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
            catch (Exception )
            {
                return Json("Error");
            }
        }
    }
}