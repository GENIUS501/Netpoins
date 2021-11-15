using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NOficinas
    {
        #region Agregar
        public int Agregar(EOficinas obj,int Usuario)
        {
            try
            {
                DOficinas db = new DOficinas();
                return db.Agregar(obj,Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Mostrar Detallado
        public EOficinas Mostrar_Detallado(int Id)
        {
            try
            {
                DOficinas db = new DOficinas();
                return db.Mostrar_Detallado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Modificar
        public int Modificar(EOficinas obj, int Usuario)
        {
            try
            {
                DOficinas db = new DOficinas();
                return db.Modificar(obj,Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Eliminar
        public int Eliminar(int IDOficina, int Usuario)
        {
            try
            {
                DOficinas db = new DOficinas();
                return db.Eliminar(IDOficina,Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Listar
        public List<EOficinas> Mostrar()
        {
            try
            {
                DOficinas db = new DOficinas();
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
