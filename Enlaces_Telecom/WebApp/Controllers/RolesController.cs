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
    public class RolesController : Controller
    {

        private IRolesService RolesService;

        public RolesController(IRolesService rolesService)
        {
            RolesService = rolesService;
        }



        // GET: Roles
        public ActionResult Index()
        {
            var Roles = RolesService.ObtenerLista(null);
            if (TempData.ContainsKey("msg")) ViewData["msg"] = TempData["msg"].ToString();
            return View(Roles);
        }

        public ActionResult Edit(int? id)
        {
          
            var entity = new RolesEdit() { roles = new RolesEntity() };
            try
            {
                ViewBag.Form = false;
                if (id.HasValue)
                {
                    //editar
                    ViewBag.Form = true;
                    entity.roles = RolesService.ObtenerDetalle(id);
                }

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View(entity);
        }

        [HttpPost]
        public ActionResult Save(RolesEntity entity)
        {
            try
            {
                var result = new DBEntity();

                if (entity.IdRol.HasValue)
                {
                    result = RolesService.Actualizar(entity);
                }
                else
                {
                    result = RolesService.Insertar(entity);
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
                var result = RolesService.Eliminar(new RolesEntity { IdRol = id });
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