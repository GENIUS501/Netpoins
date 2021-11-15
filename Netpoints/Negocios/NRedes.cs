using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NRedes
    {
        #region Agregar
        public int Agregar(ERedes obj,int IdUsuario)
        {
            try
            {
                DRedes db = new DRedes();
                return db.Agregar(obj,IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Mostrar Detallado
        public ERedes Mostrar_Detallado(int Id)
        {
            try
            {
                DRedes db = new DRedes();
                return db.Mostrar_Detallado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Modificar
        public int Modificar(ERedes obj, int IdUsuario)
        {
            try
            {
                DRedes db = new DRedes();
                return db.Modificar(obj,IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Eliminar
        public int Eliminar(int IDRedes, int IdUsuario)
        {
            try
            {
                DRedes db = new DRedes();
                return db.Eliminar(IDRedes, IdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Listar
        public List<ERedes> Mostrar()
        {
            try
            {
                DRedes db = new DRedes();
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
