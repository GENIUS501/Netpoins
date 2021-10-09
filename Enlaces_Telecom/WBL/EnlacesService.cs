using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IEnlacesService : IDisposable
    {
        List<EnlacesEntity> ObtenerLista(int? IdEnlace);
        EnlacesEntity ObtenerDetalle(int? IdEnlace);
        List<EnlacesEntity> Obtenerddl();
        DBEntity Insertar(EnlacesEntity entity);
        DBEntity Actualizar(EnlacesEntity entity);
        DBEntity Eliminar(EnlacesEntity entity);
    }

    public class EnlacesService : IEnlacesService
    {
        public IBD sql = new BD("conn");
        public void Dispose()
        {
            sql = null;
        }

        public List<EnlacesEntity> ObtenerLista(int? IdEnlace)
        {
            try
            {
                var result = sql.Query<EnlacesEntity, OficinasEntity, RedesEntity, ProveedoresEntity>("EnlacesObtener", "IdOficina, IdRed, IdProveedor", new
                {
                   IdEnlace
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EnlacesEntity ObtenerDetalle(int? IdEnlace)
        {
            try
            {
                var result = sql.QueryFirst<EnlacesEntity>("EnlacesObtener", new
                {
                    IdEnlace
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EnlacesEntity> Obtenerddl()
        {
            try
            {
                var result = sql.Query<EnlacesEntity>("EnlacesLista");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DBEntity Insertar(EnlacesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("EnlacesInsertar", new
                {
                    entity.IdOficina,
                    entity.IdRed,
                    entity.IdProveedor,
                    entity.Comentario,
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }

        public DBEntity Actualizar(EnlacesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("EnlacesActualizar", new
                {
                    entity.IdEnlace,
                    entity.IdOficina,
                    entity.IdRed,
                    entity.IdProveedor,
                    entity.Comentario,
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }

        public DBEntity Eliminar(EnlacesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("EnlacesEliminar", new
                {
                    entity.IdEnlace,
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
