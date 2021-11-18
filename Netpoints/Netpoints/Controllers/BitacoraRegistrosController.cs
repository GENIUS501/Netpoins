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
    public class BitacoraRegistrosController : Controller
    {
        [AuthorizeUser(idmodulo: 9)]
        // GET: BitacoraRegistros
        public ActionResult Index()
        {
            NBitacoraRegistro Negocios = new NBitacoraRegistro();
            List<EBitacoraRegistros> Lista = new List<EBitacoraRegistros>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
    }
}