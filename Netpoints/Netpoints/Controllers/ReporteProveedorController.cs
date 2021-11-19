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
        public ActionResult Index(string Proveedor)
        {
            NEnlaces Negocios = new NEnlaces();
            List<EEnlaces> Lista = new List<EEnlaces>();
            NProveedores Proveedores = new NProveedores();
            List<EProveedores> Lista2 = new List<EProveedores>();
            Lista2 = Proveedores.Mostrar();
            ViewBag.ddlProveedores = Lista2;
            if (Proveedor != null && Proveedor != "")
            {
                Lista = Negocios.MostrarProveedor(int.Parse(Proveedor));
                return View(Lista);
            }
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
    }
}