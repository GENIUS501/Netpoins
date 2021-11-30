using Entidades;
using Negocios;
using Netpoints.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //Recibe el usuario y la contrasena del sistema
        public ActionResult Login(LoginViewModel Modelo)
        {
            try
            {
                //Encripta la contrasena para realizar la prueba
                string passa = Helpers.Helper.EncodePassword(string.Concat(Modelo.Usuario.ToString(), Modelo.Pass.ToString()));
                //Se consulta la base de datos con la contrasena y el usuario dados por usuario 
                NUsuario Negocios = new NUsuario();
                var UsuarioLogueado = Negocios.Login(Modelo.Usuario, passa);
                //Si la base no devolvio la entidad usuarios llena es por que el usuario o la clave esta mal
                if (UsuarioLogueado == null)
                {
                    //Expresa el error a la vista
                    //ViewBag.Error = "Usuario o Contrasena invalida!!!";
                    //Notifica que la clave esta incorrecta
                    //TempData["msg"] = "<script>alert('Usuario o contrasena invalida!!!');</script>";
                    return View();
                }
                else
                {
                    ////Crea la sesion con la que el sistema validara
                    NBitacoraRegistro Registro = new NBitacoraRegistro();
                    EBitacoraRegistros Entidad = new EBitacoraRegistros();
                    Entidad.IdUsuario = UsuarioLogueado.IdUsuario;
                    Entidad.FechaHoraIngreso = DateTime.Now;
                    int id_sesion = Registro.Agregar(Entidad);
                    //if (id_sesion == 0)
                    //{
                    //    TempData["msg"] = "<script>alert('Error al ingresar al sistema!!!');</script>";

                    //    return View();
                    //}
                    Session["id_sesion"] = id_sesion;
                    Session["User"] = UsuarioLogueado;
                    Session["Usuario"] = UsuarioLogueado.Usuario;
                    //TempData["msg"] = "<script>alert('Bienvenido " + Ent_Unsuario.NickName + "');</script>";
                    //Redirige a la pagina principal del sistema
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Login", "Acceso");
                //   return View();
            }
        }
    }
}