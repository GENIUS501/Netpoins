using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBL;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UsuariosController : Controller
    {
        private IUsuariosService UsuariosService;
        private IRolesService RolesService;
        public UsuariosController(IUsuariosService usuariosService, IRolesService rolesService)
        {
            UsuariosService = usuariosService;
            RolesService = rolesService;
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            var Usuarios = UsuariosService.ObtenerLista(null);
            if (TempData.ContainsKey("msg")) ViewData["msg"] = TempData["msg"].ToString();
            return View(Usuarios);
        }

        public ActionResult Edit(int? id)
        {
          var entity = new UsuariosEdit() { usuarios = new UsuariosEntity() };
            try
            {
                ViewBag.Form = false;
                if (id.HasValue)
                {
                    //editar
                    ViewBag.Form = true;
                    entity.usuarios = UsuariosService.ObtenerDetalle(id);
                }
                entity.ddlRoles = RolesService.Obtenerddl();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View(entity);
        }

        [HttpPost]
        public ActionResult Save(UsuariosEntity entity)
        {
            try
            {
                var result = new DBEntity();

                if (entity.IdUsuario.HasValue)
                {
                    result = UsuariosService.Actualizar(entity);
                }
                else
                {
                    result = UsuariosService.Insertar(entity);
                }
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new DBEntity { CodeError = ex.HResult, MsgError = ex.Message });
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = UsuariosService.Eliminar(new UsuariosEntity { IdUsuario = id });
                TempData["msg"] = "0";

                if (result.CodeError != 0) throw new Exception(result.MsgError);

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}