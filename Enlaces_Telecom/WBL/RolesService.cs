using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IRolesService : IDisposable
    {
        List<RolesEntity> ObtenerLista(int? IdRol);
        RolesEntity ObtenerDetalle(int? IdRol);
        List<RolesEntity> Obtenerddl();
        DBEntity Insertar(RolesEntity entity);
        DBEntity Actualizar(RolesEntity entity);
        DBEntity Eliminar(RolesEntity entity);

    }

    public class RolesService : IRolesService
    {
        public IBD sql = new BD("conn");
        public void Dispose()
        {
            sql = null;
        }

        public List<RolesEntity> ObtenerLista(int? IdRol)
        {
            try
            {
                var result = sql.Query<RolesEntity>("RolesObtener", new
                {
                    IdRol
                });
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public RolesEntity ObtenerDetalle(int? IdRol)
        {
            try
            {
                var result = sql.QueryFirst<RolesEntity>("RolesObtener", new
                {
                    IdRol
                });
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<RolesEntity> Obtenerddl()
        {
            try
            {
                var result = sql.Query<RolesEntity>("RolesLista");
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DBEntity Insertar(RolesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("RolesInsertar", new
                {
                    Rol = entity.Rol,
                    entity.Descripcion,
                    entity.Estado
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }
        public DBEntity Actualizar(RolesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("RolesActualizar", new
                {
                    entity.IdRol,
                    entity.Rol,
                    entity.Descripcion,
                    entity.Estado
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }


        public DBEntity Eliminar(RolesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("RolesEliminar", new
                {
                    entity.IdRol,

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
