using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class AcercaDeController : Controller
    {
        // GET: AcercaDe
        public ActionResult Index()
        {
            ViewBag.Message = "Descripcion de la aplicación";
            return View();
        }
    }
}