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
    public class ReporteUbicacionController : Controller
    {
        [AuthorizeUser(idmodulo: 7)]
        // GET: ReporteUbicacion
        public ActionResult Index(string Provincia)
        {
            NEnlaces Negocios = new NEnlaces();
            List<EEnlaces> Lista = new List<EEnlaces>();
            if (Provincia != null)
            {
                Lista = Negocios.Mostrar(Provincia);
            }
            else
            {
                Lista = Negocios.Mostrar();
            }
            return View(Lista);
        }
    }
}