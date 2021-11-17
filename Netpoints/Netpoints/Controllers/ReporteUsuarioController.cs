using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class ReporteUsuarioController : Controller
    {
        // GET: ReporteUsuario
        public ActionResult Index()
        {
            NUsuario Negocios = new NUsuario();
            List<EUsuario> Lista = new List<EUsuario>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
    }
}