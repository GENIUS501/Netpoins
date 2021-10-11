﻿using DataAcces;
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
        public int Agregar(ERol obj)
        {
            try
            {
                DRol db = new DRol();
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
    }
}
