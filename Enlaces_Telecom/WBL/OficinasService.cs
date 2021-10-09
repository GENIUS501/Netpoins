using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IOficinasService : IDisposable
    {
        List<OficinasEntity> ObtenerLista(int? IdOficina);
        OficinasEntity ObtenerDetalle(int? IdOficina);
        List<OficinasEntity> Obtenerddl();
        DBEntity Insertar(OficinasEntity entity);
        DBEntity Actualizar(OficinasEntity entity);
        DBEntity Eliminar(OficinasEntity entity);
    }

    public class OficinasService : IOficinasService
    {
        public IBD sql = new BD("conn");
        public void Dispose()
        {
            sql = null;
        }

        public List<OficinasEntity> ObtenerLista(int? IdOficina)
        {
            try
            {
                var result = sql.Query<OficinasEntity>("OficinasObtener", new
                {
                    IdOficina
                });
                return result;
            }
            catch (Exception ex)
            {
              throw;
            }
        }
        public OficinasEntity ObtenerDetalle(int? IdOficina)
        {
            try
            {
                var result = sql.QueryFirst<OficinasEntity>("OficinasObtener", new
                {
                    IdOficina
                });
                return result;
            }
            catch (Exception ex)
            {
              throw;
            }
        }

        public List<OficinasEntity> Obtenerddl()
        {
            try
            {
                var result = sql.Query<OficinasEntity>("OficinasLista");
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DBEntity Insertar(OficinasEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("OficinasInsertar", new
                {
                    entity.NombreOficina,
                    entity.UE,
                    entity.Provincia,
                    entity.Comentario,
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }
        public DBEntity Actualizar(OficinasEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("OficinasActualizar", new
                {
                    entity.IdOficina,
                    entity.NombreOficina,
                    entity.UE,
                    entity.Provincia,
                    entity.Comentario,
                });
                return result;
            }
            catch (Exception ex)
            {
                return new DBEntity { CodeError = ex.HResult, MsgError = ex.Message };
            }
        }

        public DBEntity Eliminar(OficinasEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("OficinasEliminar", new
                {
                    entity.IdOficina,
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
