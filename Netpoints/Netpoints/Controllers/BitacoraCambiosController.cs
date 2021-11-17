using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class BitacoraCambiosController : Controller
    {
        // GET: BitacoraCambios
        public ActionResult Index()
        {
            NBitacoraCambios Negocios = new NBitacoraCambios();
            List<EBitacoraCambios> Lista = new List<EBitacoraCambios>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        #region Traer el nombre del Rol
        public static string NombreUsuario(string id)
        {
            try
            {
                NUsuario Negocios = new NUsuario();
                var user = Negocios.Mostrar_Detallado(int.Parse(id));
                return user.Usuario.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}