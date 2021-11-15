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
        public int Agregar(EUsuario obj,int Usuario)
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Agregar(obj,Usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Mostrar Detallado
        public EUsuario Mostrar_Detallado(int Id)
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Mostrar_Detallado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Modificar
        public int Modificar(EUsuario obj,int idusuario)
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Modificar(obj,idusuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Desactivar(int IdUsuario,int idusuarioses)
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Desactivar(IdUsuario,idusuarioses);
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

        #region Metodos Personalizados
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
        public async Task<bool> Verificar(string Id)
        {
            try
            {
                DUsuario db = new DUsuario();
                return await db.Verificar(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public  EUsuario Login(string Usuario, string Pass)
        {
            try
            {
                DUsuario db = new DUsuario();
                return db.Login(Usuario,Pass);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
