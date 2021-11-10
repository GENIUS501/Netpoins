using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NRoles_Permisos
    {
        #region Agregar
        public int Agregar(List<ERoles_Permisos> obj)
        {
            try
            {
                DRoles_Permisos db = new DRoles_Permisos();
                return db.Agregar(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Mostrar Detallado
        public ERoles_Permisos Mostrar_Detallado(int Id)
        {
            try
            {
                DRoles_Permisos db = new DRoles_Permisos();
                return db.Mostrar_Detallado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Modificar
        public int Modificar(ERoles_Permisos obj)
        {
            try
            {
                DRoles_Permisos db = new DRoles_Permisos();
                return db.Modificar(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Eliminar
        public int Eliminar(int Idrol)
        {
            try
            {
                DRoles_Permisos db = new DRoles_Permisos();
                return db.Eliminar(Idrol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Listar
        public List<ERoles_Permisos> Mostrar()
        {
            try
            {
                DRoles_Permisos db = new DRoles_Permisos();
                return db.Mostrar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
