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
    public class RedesController : Controller
    {
        //private IRedesService RedesService;
        //private IProveedoresService ProveedoresService;
        //public RedesController(IRedesService redesService)
        //{
        //    RedesService = redesService;
        //}

        //// GET: Redes
        //public ActionResult Index()
        //{
        //    var Redes = RedesService.ObtenerLista(null);
        //    if (TempData.ContainsKey("msg")) ViewData["msg"] = TempData["msg"].ToString();
        //    return View(Redes);
        //}

        //public ActionResult Edit(int? id)
        //{
        //    var entity = new RedesEdit() { redes = new RedesEntity() };
        //    try
        //    {
        //        ViewBag.Form = false;
        //        if (id.HasValue)
        //        {
        //            //editar
        //            ViewBag.Form = true;
        //            entity.redes = RedesService.ObtenerDetalle(id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //    return View(entity);
        //}

        //[HttpPost]
        //public ActionResult Save(RedesEntity entity)
        //{
        //    try
        //    {
        //        var result = new DBEntity();

        //        if (entity.IdRed.HasValue)
        //        {
        //            result = RedesService.Actualizar(entity);
        //        }
        //        else
        //        {
        //            result = RedesService.Insertar(entity);
        //        }
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new DBEntity { CodeError = ex.HResult, MsgError = ex.Message });
        //    }
        //}


        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var result = RedesService.Eliminar(new RedesEntity { IdRed = id });
        //        TempData["msg"] = "0";

        //        if (result.CodeError != 0) throw new Exception(result.MsgError);

        //        return RedirectToAction("index");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}
    }
}