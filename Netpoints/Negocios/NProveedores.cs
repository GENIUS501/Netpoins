using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NProveedores
    {
        #region Agregar
        public int Agregar(EProveedores obj, int Usuario)
        {
            try
            {
                DProveedores db = new DProveedores();
                return db.Agregar(obj, Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Mostrar Detallado
        public EProveedores Mostrar_Detallado(int Id)
        {
            try
            {
                DProveedores db = new DProveedores();
                return db.Mostrar_Detallado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Modificar
        public int Modificar(EProveedores obj, int Usuario)
        {
            try
            {
                DProveedores db = new DProveedores();
                return db.Modificar(obj, Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Eliminar
        public int Eliminar(int IDProveedores,int Usuario)
        {
            try
            {
                DProveedores db = new DProveedores();
                return db.Eliminar(IDProveedores,Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Listar
        public List<EProveedores> Mostrar()
        {
            try
            {
                DProveedores db = new DProveedores();
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
