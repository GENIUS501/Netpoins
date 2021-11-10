using Entidades;
using Negocios;
using Netpoints.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Netpoints.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            NRol Negocios = new NRol();
            List<ERol> Lista = new List<ERol>();
            Lista = Negocios.Mostrar();
            return View(Lista);
        }
        public ActionResult Agregar(ERolViewModel Modelo)
        {
            try
            {
                if (Modelo.Rol == null && Modelo.Descripcion == null)
                {
                    return View();
                }
                NRol Negocios = new NRol();
                ERol Rol = new ERol();
                Rol.Rol = Modelo.Rol;
                Rol.Descripcion = Modelo.Descripcion;
                Rol.Estado = false;
                if (Modelo.Estado == "on")
                {
                    Rol.Estado = true;
                }
                int FilasAfectadas = Negocios.Agregar(Rol);
                Modelo.IdRol = FilasAfectadas;
                var Permisos = Grabar(Modelo);
                NRoles_Permisos Negocios_Permisos = new NRoles_Permisos();
                int FilasAfectadasPermisos = Negocios_Permisos.Agregar(Permisos);
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
        #region Metodo para guardar los permisos
        private List<ERoles_Permisos> Grabar(ERolViewModel Modelo)
        {
            try
            {
                List<ERoles_Permisos> permisos = new List<ERoles_Permisos>();
                #region Modulo Usuario 1
                if (Modelo.radusu == false)
                {

                }
                else
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 1;
                    //Agregar
                    if (Modelo.checkusuA == true)
                    {
                        perm.Agregar = "S";
                    }
                    else
                    {
                        perm.Agregar = "N";
                    }
                    //Actualizar
                    if (Modelo.checkusuE == true)
                    {
                        perm.Modificar = "S";
                    }
                    else
                    {
                        perm.Modificar = "N";
                    }
                    //Eliminar
                    if (Modelo.checkusuD == true)
                    {
                        perm.Eliminar = "S";
                    }
                    else
                    {
                        perm.Eliminar = "N";
                    }

                    //Grabar
                    permisos.Add(perm);
                }
                #endregion

                #region Modulo Roles 2
                if (Modelo.radrol == false)
                {

                }
                else
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 2;
                    //Agregar
                    if (Modelo.checkrolA == true)
                    {
                        perm.Agregar = "S";
                    }
                    else
                    {
                        perm.Agregar = "N";
                    }
                    //Actualizar
                    if (Modelo.checkrolE == true)
                    {
                        perm.Modificar = "S";
                    }
                    else
                    {
                        perm.Modificar = "N";
                    }
                    //Eliminar
                    if (Modelo.checkrolD == true)
                    {
                        perm.Eliminar = "S";
                    }
                    else
                    {
                        perm.Eliminar = "N";
                    }

                    //Grabar
                    permisos.Add(perm);
                }
                #endregion

                #region Modulo Proveedores 3
                if (Modelo.radprov == false)
                {

                }
                else
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 3;
                    //Agregar
                    if (Modelo.checkprovA == true)
                    {
                        perm.Agregar = "S";
                    }
                    else
                    {
                        perm.Agregar = "N";
                    }
                    //Actualizar
                    if (Modelo.checkprovE == true)
                    {
                        perm.Modificar = "S";
                    }
                    else
                    {
                        perm.Modificar = "N";
                    }
                    //Eliminar
                    if (Modelo.checkprovD == true)
                    {
                        perm.Eliminar = "S";
                    }
                    else
                    {
                        perm.Eliminar = "N";
                    }

                    //Grabar
                    permisos.Add(perm);
                }
                #endregion

                #region Modulo Redes 4
                if (Modelo.radprov == false)
                {

                }
                else
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 4;
                    //Agregar
                    if (Modelo.checkredA == true)
                    {
                        perm.Agregar = "S";
                    }
                    else
                    {
                        perm.Agregar = "N";
                    }
                    //Actualizar
                    if (Modelo.checkredE == true)
                    {
                        perm.Modificar = "S";
                    }
                    else
                    {
                        perm.Modificar = "N";
                    }
                    //Eliminar
                    if (Modelo.checkredD == true)
                    {
                        perm.Eliminar = "S";
                    }
                    else
                    {
                        perm.Eliminar = "N";
                    }

                    //Grabar
                    permisos.Add(perm);
                }
                #endregion

                #region Modulo Sitios 5
                if (Modelo.radsitios == false)
                {

                }
                else
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 5;
                    //Agregar
                    if (Modelo.checksitA == true)
                    {
                        perm.Agregar = "S";
                    }
                    else
                    {
                        perm.Agregar = "N";
                    }
                    //Actualizar
                    if (Modelo.checksitE == true)
                    {
                        perm.Modificar = "S";
                    }
                    else
                    {
                        perm.Modificar = "N";
                    }
                    //Eliminar
                    if (Modelo.checksitD == true)
                    {
                        perm.Eliminar = "S";
                    }
                    else
                    {
                        perm.Eliminar = "N";
                    }

                    //Grabar
                    permisos.Add(perm);
                }
                #endregion

                #region Reporte Proveedores 6
                if (Modelo.repproveedor == true)
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 6;
                    perm.Agregar = "N";
                    perm.Eliminar = "N";
                    perm.Modificar = "N";
                    permisos.Add(perm);
                }
                #endregion

                #region Reporte Ubicacion 7
                if (Modelo.repubicacion == true)
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 7;
                    perm.Agregar = "N";
                    perm.Eliminar = "N";
                    perm.Modificar = "N";
                    permisos.Add(perm);
                }
                #endregion

                #region Reporte Usuarios 8
                if (Modelo.repusuarios == true)
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 8;
                    perm.Agregar = "N";
                    perm.Eliminar = "N";
                    perm.Modificar = "N";
                    permisos.Add(perm);
                }
                #endregion

                #region Bitacora registro 9
                if (Modelo.bitreg == true)
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 9;
                    perm.Agregar = "N";
                    perm.Eliminar = "N";
                    perm.Modificar = "N";
                    permisos.Add(perm);
                }
                #endregion

                #region Bitacora Cambios 10
                if (Modelo.repproveedor == true)
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 10;
                    perm.Agregar = "N";
                    perm.Eliminar = "N";
                    perm.Modificar = "N";
                    permisos.Add(perm);
                }
                #endregion

                #region Modulo enlaces 11
                if (Modelo.radenlace == false)
                {

                }
                else
                {
                    ERoles_Permisos perm = new ERoles_Permisos();
                    perm.Id_Rol = Modelo.IdRol;
                    perm.Modulo = 11;
                    //Agregar
                    if (Modelo.checkenlA == true)
                    {
                        perm.Agregar = "S";
                    }
                    else
                    {
                        perm.Agregar = "N";
                    }
                    //Actualizar
                    if (Modelo.checkenlE == true)
                    {
                        perm.Modificar = "S";
                    }
                    else
                    {
                        perm.Modificar = "N";
                    }
                    //Eliminar
                    if (Modelo.checkenlD == true)
                    {
                        perm.Eliminar = "S";
                    }
                    else
                    {
                        perm.Eliminar = "N";
                    }

                    //Grabar
                    permisos.Add(perm);
                }
                #endregion

                return permisos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodo para cargar los permisos
        public ERolViewModel CargarPermisos(ERolViewModel Model)
        {
            try
            {
                NRoles_Permisos Negocios = new NRoles_Permisos();
                var Permisos = Negocios.Mostrar(Model.IdRol);
                foreach (var list in Permisos)
                {
                    #region Usuarios
                    if (list.Modulo == 1)
                    {
                        Model.radusu = true;              
                        if (list.Agregar == "S")
                        {
                            Model.checkusuA = true;
                        }
                        if (list.Modificar == "S")
                        {
                            Model.checkusuE = true;
                        }
                        if (list.Eliminar == "S")
                        {
                            Model.checkusuD = true;
                        }
                    }
                    #endregion

                    #region Rol
                    if (list.Modulo == 2)
                    {
                        Model.radrol = true;
                        if (list.Agregar == "S")
                        {
                            Model.checkrolA = true;
                        }
                        if (list.Modificar == "S")
                        {
                            Model.checkrolE = true;
                        }
                        if (list.Eliminar == "S")
                        {
                            Model.checkrolD = true;
                        }
                    }
                    #endregion

                    #region Proveedores
                    if (list.Modulo == 3)
                    {
                        Model.radprov = true;
                        if (list.Agregar == "S")
                        {
                            Model.checkprovA = true;
                        }
                        if (list.Modificar == "S")
                        {
                            Model.checkprovE = true;
                        }
                        if (list.Eliminar == "S")
                        {
                            Model.checkprovD = true;
                        }
                    }
                    #endregion

                    #region Redes
                    if (list.Modulo == 4)
                    {
                        Model.radred = true;
                        if (list.Agregar == "S")
                        {
                            Model.checkredA = true;
                        }
                        if (list.Modificar == "S")
                        {
                            Model.checkredE = true;
                        }
                        if (list.Eliminar == "S")
                        {
                            Model.checkredD = true;
                        }
                    }
                    #endregion

                    #region Sitios
                    if (list.Modulo == 5)
                    {
                        Model.radsitios = true;
                        if (list.Agregar == "S")
                        {
                            Model.checksitA = true;
                        }
                        if (list.Modificar == "S")
                        {
                            Model.checksitE = true;
                        }
                        if (list.Eliminar == "S")
                        {
                            Model.checksitD = true;
                        }
                    }
                    #endregion

                    #region Reporte de proveedores
                    if (list.Modulo == 6)
                    {
                        Model.repproveedor = true;
                    }
                    #endregion

                    #region Reporte de ubicacion
                    if (list.Modulo == 7)
                    {
                        Model.repubicacion = true;
                    }
                    #endregion

                    #region Reporte de usuarios
                    if (list.Modulo == 8)
                    {
                        Model.repusuarios = true;
                    }
                    #endregion

                    #region Bitacora de registro
                    if (list.Modulo == 9)
                    {
                        Model.bitreg = true;
                    }
                    #endregion
                    #region Bitacora de cambios
                    if (list.Modulo == 10)
                    {
                        Model.bitcam = true;
                    }
                    #endregion
                    #region Enlaces
                    if (list.Modulo == 11)
                    {
                        Model.radenlace = true;
                        if (list.Agregar == "S")
                        {
                            Model.checkenlA = true;
                        }
                        if (list.Modificar == "S")
                        {
                            Model.checkenlE = true;
                        }
                        if (list.Eliminar == "S")
                        {
                            Model.checkenlD = true;
                        }
                    }
                    #endregion
                }
                return Model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public ActionResult Edit(string Id)
        {
            NRol Negocios = new NRol();
            var Rol = Negocios.Mostrar_Detallado(int.Parse(Id));
            ERolViewModel RolV = new ERolViewModel();
            RolV.IdRol = Rol.IdRol;
            RolV.Rol = Rol.Rol;
            RolV.Descripcion = Rol.Descripcion;
            RolV.Estado = Rol.Estado.ToString();
            var RolFinal = CargarPermisos(RolV);
            return View(RolFinal);
        }
        //Le indica al metodo que reciba los datos por el metodo post
        [HttpPost]
        ////Evita que se inicie de otro formulario
        //[ValidateAntiForgeryToken]
        //Action result es el tipo de dato que retorna la funcion
        public ActionResult Edit(ERolViewModel Model)
        {
            try
            {
                if (Model.Rol == null || Model.Descripcion == null)
                {
                    return View();
                }
                NRol Negocios = new NRol();
                ERol Rol = new ERol();
                Rol.IdRol = Model.IdRol;
                Rol.Rol = Model.Rol;
                Rol.Descripcion = Model.Descripcion;
                Rol.Estado = false;
                if (Model.Estado == "False")
                {
                    Rol.Estado = true;
                }
                int FilasAfectadas = Negocios.Modificar(Rol);
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
                NRol Negocios = new NRol();
                Negocios.Desactivar(int.Parse(id));
                TempData["msg"] = "0";
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Verificar(string id)
        {
            try
            {
                NRol Negocios = new NRol();
                bool NoExiste = await Negocios.Verificar(id);
                if (NoExiste)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
    }
}