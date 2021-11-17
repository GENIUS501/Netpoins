using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NEnlaces
    {
        #region Agregar
        public int Agregar(EEnlaces obj, int Usuario)
        {
            try
            {
                DEnlaces db = new DEnlaces();
                return db.Agregar(obj, Usuario);
            }
            catch (Exception ex) { 
                throw ex; 
            }
        }
        #endregion

        #region Mostrar Detallado
        public EEnlaces Mostrar_Detallado(int Id)
        {
            try
            {
                DEnlaces db = new DEnlaces();
                return db.Mostrar_Detallado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Modificar
        public int Modificar(EEnlaces obj,int Usuario)
        {
            try
            {
                DEnlaces db = new DEnlaces();
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
                DEnlaces db = new DEnlaces();
                return db.Eliminar(IDOficina,Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Listar
        public List<EEnlaces> Mostrar()
        {
            try
            {
                DEnlaces db = new DEnlaces();
                return db.Mostrar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EEnlaces> Mostrar(string Provincia)
        {
            try
            {
                DEnlaces db = new DEnlaces();
                return db.Mostrar(Provincia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
