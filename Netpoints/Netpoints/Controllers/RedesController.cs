﻿using Entidades;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class RedesController : Controller
    {
        public ActionResult Index()
        {
            NRedes Negocios = new NRedes();
            List<ERedes> Lista = new List<ERedes>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        public ActionResult Agregar(ERedes Modelo)
        {
            try
            {
                if (Modelo.Linea == null && Modelo.Gateway == null)
                {
                    return View();
                }
                NRedes Negocios = new NRedes();
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
            NRedes Negocios = new NRedes();
            var Modelo = Negocios.Mostrar_Detallado(int.Parse(Id));
            return View(Modelo);
        }

        [HttpPost]
        public ActionResult Edit(ERedes Modelo)
        {
            try
            {
                NRedes Negocios = new NRedes();
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
                NRedes Negocios = new NRedes();
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