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
    public class EnlacesController : Controller
    {
        //private IEnlacesService EnlacesService;
        //private IOficinasService OficinasService;
        //private IRedesService RedesService;
        //private IProveedoresService ProveedoresService;

        //public EnlacesController(IEnlacesService enlacesService, IOficinasService oficinasService, IRedesService redesService, IProveedoresService proveedoresService)
        //{
        //    EnlacesService = enlacesService;
        //    OficinasService = oficinasService;
        //    RedesService = redesService;
        //    ProveedoresService = proveedoresService;
        //}

        //// GET: Enlaces
        //public ActionResult Index()
        //{
        //    var Enlaces = EnlacesService.ObtenerLista(null);
        //    if (TempData.ContainsKey("msg")) ViewData["msg"] = TempData["msg"].ToString();
        //    return View(Enlaces);
        //}
        // public ActionResult Edit(int? id)
        //{
        //    var entity = new EnlacesEdit() { enlaces = new EnlacesEntity() };
        //    try
        //    {
        //        ViewBag.Form = false;
        //        if (id.HasValue)
        //        {
        //            //editar
        //            ViewBag.Form = true;
        //            entity.enlaces = EnlacesService.ObtenerDetalle(id);
        //        }
        //        entity.ddlOficinas = OficinasService.Obtenerddl();
        //        entity.ddlRedes = RedesService.Obtenerddl();
        //        entity.ddlProveedores = ProveedoresService.Obtenerddl();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //    return View(entity);
        //}

        //[HttpPost]
        //public ActionResult Save(EnlacesEntity entity)
        //{
        //    try
        //    {
        //        var result = new DBEntity();

        //        if (entity.IdEnlace.HasValue)
        //        {
        //            result = EnlacesService.Actualizar(entity);
        //        }
        //        else
        //        {
        //            result = EnlacesService.Insertar(entity);
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
        //        var result = EnlacesService.Eliminar(new EnlacesEntity { IdEnlace = id });
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