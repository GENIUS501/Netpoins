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
    public class ReporteProveedorController : Controller
    {
        // GET: ReporteProveedor
        [AuthorizeUser(idmodulo: 6)]
        public ActionResult Index()
        {
            NProveedores Negocios = new NProveedores();
            List<EProveedores> Lista = new List<EProveedores>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
    }
}