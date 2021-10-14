﻿using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class OficinasController : Controller
    {
        // GET: Oficinas
        public ActionResult Index()
        {
            NOficinas Negocios = new NOficinas();
            List<EOficinas> Lista = new List<EOficinas>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        public ActionResult Agregar(EOficinas Modelo)
        {
            try
            {
                if (Modelo.NombreOficina == null && Modelo.Provincia == null)
                {
                    return View();
                }
                NOficinas Negocios = new NOficinas();
                int FilasAfectadas = Negocios.Agregar(Modelo);
                if (FilasAfectadas > 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
        [HttpGet]
        public ActionResult Edit(string Id)
        {
            NOficinas Negocios = new NOficinas();
            var Modelo = Negocios.Mostrar_Detallado(int.Parse(Id));
            return View(Modelo);
        }

        [HttpPost]
        public ActionResult Edit(EOficinas Modelo)
        {
            try
            {
                NOficinas Negocios = new NOficinas();
                int FilasAfectadas = Negocios.Modificar(Modelo);
                if (FilasAfectadas > 0)
                {
                    return Json("success");
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Json("Error");
            }
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            try
            {
                NOficinas Negocios = new NOficinas();
                int FilasAfectadas = Negocios.Eliminar(int.Parse(id));
                if (FilasAfectadas > 0)
                {
                    TempData["msg"] = "0";
                }
                else
                {
                    TempData["msg"] = "-1";
                }
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return Redirect("Index");
            }
        }
    }
}