using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IProveedoresService : IDisposable
    {
        List<ProveedoresEntity> ObtenerLista(int? IdProveedor);
        ProveedoresEntity ObtenerDetalle(int? IdProveedor);
        List<ProveedoresEntity> Obtenerddl();
        DBEntity Insertar(ProveedoresEntity entity);
        DBEntity Actualizar(ProveedoresEntity entity);
        DBEntity Eliminar(ProveedoresEntity entity);

    }

    public class ProveedoresService : IProveedoresService
    {
        public IBD sql = new BD("conn");
        public void Dispose()
        {
            sql = null;
        }

        public List<ProveedoresEntity> ObtenerLista(int? IdProveedor)
        {
            try
            {
                var result = sql.Query<ProveedoresEntity>("ProveedoresObtener", new
                {
                    IdProveedor
                });
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public ProveedoresEntity ObtenerDetalle(int? IdProveedor)
        {
            try
            {
                var result = sql.QueryFirst<ProveedoresEntity>("ProveedoresObtener", new
                {
                    IdProveedor
                });
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<ProveedoresEntity> Obtenerddl()
        {
            try
            {
                var result = sql.Query<ProveedoresEntity>("ProveedoresLista");
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DBEntity Insertar(ProveedoresEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("ProveedoresInsertar", new
                {
                    NombreEmpresa = entity.NombreEmpresa,
                    entity.Contacto,
                    entity.Telefono,
                    entity.Email,
                    entity.Comentario,
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }
        public DBEntity Actualizar(ProveedoresEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("ProveedoresActualizar", new
                {
                    entity.IdProveedor,
                    entity.NombreEmpresa,
                    entity.Contacto,
                    entity.Telefono,
                    entity.Email,
                    entity.Comentario,
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }

        public DBEntity Eliminar(ProveedoresEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("ProveedoresEliminar", new
                {
                    entity.IdProveedor,
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
