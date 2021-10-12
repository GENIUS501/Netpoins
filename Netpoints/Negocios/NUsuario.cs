using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NUsuario
    {
        #region Agregar
        public int Agregar(EUsuario obj)
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Agregar(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Mostrar Detallado
        public ERol Mostrar_Detallado(int Id)
        {
            try
            {
                DRol db = new DRol();
                return db.Mostrar_Detallado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Modificar
        public int Modificar(ERol obj)
        {
            try
            {
                DRol db = new DRol();
                return db.Modificar(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Desactivar(int IdRol)
        {
            try
            {
                DRol db = new DRol();
                return db.Desactivar(IdRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Listar
        public List<EUsuario> Mostrar()
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Mostrar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Metodos Personalisados
        public string Nombre_Rol(int Id)
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Nombre_Rol(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ERol> LlenarRoles()
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.llenarRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
