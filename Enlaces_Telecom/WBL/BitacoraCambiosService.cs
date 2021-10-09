using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IBitacoraCambiosService : IDisposable
    {
        List<BitacoraCambiosEntity> ObtenerLista(int? IdCambio);
        DBEntity Insertar(BitacoraCambiosEntity entity);
    }
    public class BitacoraCambiosService : IBitacoraCambiosService
    {
        public IBD sql = new BD("Conn");
        public void Dispose()
        {
            sql = null;
        }
        public List<BitacoraCambiosEntity> ObtenerLista(int? IdCambio)
        {
            try
            {
                var result = sql.Query<BitacoraCambiosEntity, UsuariosEntity>("BitacoraCambiosObtener", "IdUsuario", new
                { 
                 IdCambio,
                });
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DBEntity Insertar(BitacoraCambiosEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("BitacoraCambiosInsertar", new
                {
                    entity.IdCambio,
                    entity.IdUsuario,
                    entity.FechaHora,
                    entity.Tipo,
                    entity.Detalle,
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
