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
    public class ProveedoresController : Controller
    {
        private IProveedoresService ProveedoresService;
        public ProveedoresController(IProveedoresService proveedoresService)
        {
            ProveedoresService = proveedoresService;
        }
        // GET: Proveedores
        public ActionResult Index()
        {
            var Proveedores = ProveedoresService.ObtenerLista(null);
            if (TempData.ContainsKey("msg")) ViewData["msg"] = TempData["msg"].ToString();
            return View(Proveedores);
        }

        public ActionResult Edit(int? id)
        {
            var entity = new ProveedoresEdit() { proveedores = new ProveedoresEntity() };
            try
            {
                ViewBag.Form = false;
                if (id.HasValue)
                {
                    //editar
                    ViewBag.Form = true;
                    entity.proveedores = ProveedoresService.ObtenerDetalle(id);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            return View(entity);
        }

        [HttpPost]
        public ActionResult Save(ProveedoresEntity entity)
        {
            try
            {
                var result = new DBEntity();

                if (entity.IdProveedor.HasValue)
                {
                    result = ProveedoresService.Actualizar(entity);
                }
                else
                {
                    result = ProveedoresService.Insertar(entity);
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
                var result = ProveedoresService.Eliminar(new ProveedoresEntity { IdProveedor = id });
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