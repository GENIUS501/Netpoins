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
        public int Agregar(EOficinas obj)
        {
            try
            {
                DOficinas db = new DOficinas();
                return db.Agregar(obj);
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
        public int Modificar(EOficinas obj)
        {
            try
            {
                DOficinas db = new DOficinas();
                return db.Modificar(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Eliminar
        public int Eliminar(int IDOficina)
        {
            try
            {
                DOficinas db = new DOficinas();
                return db.Eliminar(IDOficina);
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
