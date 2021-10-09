using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IRedesService : IDisposable
    {
        List<RedesEntity> ObtenerLista(int? IdRed);
        RedesEntity ObtenerDetalle(int? IdRed);
        List<RedesEntity> Obtenerddl();
        DBEntity Insertar(RedesEntity entity);
        DBEntity Actualizar(RedesEntity entity);
        DBEntity Eliminar(RedesEntity entity);
    }

    public class RedesService : IRedesService
    {
        public IBD sql = new BD("conn");
        public void Dispose()
        {
            sql = null;
        }

        public List<RedesEntity> ObtenerLista(int? IdRed)
        {
            try
            {
                var result = sql.Query<RedesEntity>("RedesObtener", new
                {
                    IdRed
                });
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public RedesEntity ObtenerDetalle(int? IdRed)
        {
            try
            {
                var result = sql.QueryFirst<RedesEntity>("RedesObtener", new
                {
                    IdRed
                });
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<RedesEntity> Obtenerddl()
        {
            try
            {
                var result = sql.Query<RedesEntity>("RedesLista");
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DBEntity Insertar(RedesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("RedesInsertar", new
                {
                    entity.Linea,
                    entity.Gateway,
                    entity.Interface,
                    entity.TipoEnlace,
                    entity.Bandwidth,
                    entity.MedioEnlace,
                    entity.Comentario,
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }
        public DBEntity Actualizar(RedesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("RedesActualizar", new
                {
                    entity.IdRed,
                    entity.Linea,
                    entity.Gateway,
                    entity.Interface,
                    entity.TipoEnlace,
                    entity.Bandwidth,
                    entity.MedioEnlace,
                    entity.Comentario,
                });

                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }
        public DBEntity Eliminar(RedesEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("RedesEliminar", new
                {
                    entity.IdRed,
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
