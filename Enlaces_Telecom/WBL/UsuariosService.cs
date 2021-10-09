using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IUsuariosService : IDisposable
    {
        List<UsuariosEntity> ObtenerLista(int? IdUsuario);
        UsuariosEntity ObtenerDetalle(int? IdUsuario);
        List<UsuariosEntity> Obtenerddl();
        DBEntity Insertar(UsuariosEntity entity);
        DBEntity Actualizar(UsuariosEntity entity);
        DBEntity Eliminar(UsuariosEntity entity);

    }

    public class UsuariosService : IUsuariosService

    {
        public IBD sql = new BD("conn");
        public void Dispose()
        {
            sql = null;
        }

        public List<UsuariosEntity> ObtenerLista(int? IdUsuario)
        {
            try
            {
                var result = sql.Query<UsuariosEntity, RolesEntity>("UsuariosObtener", "IdRol", new
                {
                    IdUsuario
                });
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public UsuariosEntity ObtenerDetalle(int? IdUsuario)
        {
            try
            {
                var result = sql.QueryFirst<UsuariosEntity>("UsuariosObtener", new
                {
                    IdUsuario
                });
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<UsuariosEntity> Obtenerddl()
        {
            try
            {
                var result = sql.Query<UsuariosEntity>("UsuariosLista");
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DBEntity Insertar(UsuariosEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("UsuariosInsertar", new
                {
                    entity.IdRol,
                    entity.Identificacion,
                    entity.Nombre,
                    entity.Telefono,
                    entity.Email,
                    entity.Usuario,
                    entity.Contrasena,
                    entity.Estado
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }

        public DBEntity Actualizar(UsuariosEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("UsuariosActualizar", new
                {
                    entity.IdRol,
                    entity.IdUsuario,
                    entity.Identificacion,
                    entity.Nombre,
                    entity.Telefono,
                    entity.Email,
                    entity.Usuario,
                    entity.Contrasena,
                    entity.Estado

                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }

        public DBEntity Eliminar(UsuariosEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("UsuariosEliminar", new
                {
                    entity.IdUsuario

                });
                return result;
            }
            catch (Exception ex)
            {

                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }

    }
}
