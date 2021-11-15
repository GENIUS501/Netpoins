using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NRol
    {
        #region Agregar
        public int Agregar(ERol obj, int Usuario)
        {
            try
            {
                DRol db = new DRol();
                return db.Agregar(obj,Usuario);
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
        public int Modificar(ERol obj, int Usuario)
        {
            try
            {
                DRol db = new DRol();
                return db.Modificar(obj,Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Desactivar(int IdRol,int Usuario)
        {
            try
            {
                DRol db = new DRol();
                return db.Desactivar(IdRol, Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Listar
        public List<ERol> Mostrar()
        {
            try
            {
                DRol db = new DRol();
                return db.Mostrar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodos Personalizados
        public async Task<bool> Verificar(string Id)
        {
            try
            {
                DRol db = new DRol();
                return await db.Verificar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
