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
    public class ReporteUsuarioController : Controller
    {
        [AuthorizeUser(idmodulo: 8)]
        // GET: ReporteUsuario
        public ActionResult Index(string Idrol)
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
            List<EUsuario> Lista = new List<EUsuario>();
            if (Idrol != null)
            {
                Lista = Negocios.Mostrar(int.Parse(Idrol));
            }
            else
            {
                Lista = Negocios.Mostrar();
            }
            return View(Lista);
        }
    }
}