using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IBitacoraRegistrosService : IDisposable
    {
        List<BitacoraRegistrosEntity> ObtenerLista(int? IdRegistro);
        DBEntity Insertar(BitacoraRegistrosEntity entity);
    }
    public class BitacoraRegistrosService : IBitacoraRegistrosService
    {
        public IBD sql = new BD("Conn");
        public void Dispose()
        {
            sql = null;
        }
        public List<BitacoraRegistrosEntity> ObtenerLista(int? IdRegistro)
        {
            try
            {
                var result = sql.Query<BitacoraRegistrosEntity, UsuariosEntity>("BitacoraRegistrosObtener", "IdUsuario", new
                {
                    IdRegistro,
                });
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public DBEntity Insertar(BitacoraRegistrosEntity entity)
        {
            try
            {
                var result = sql.QueryExecute("BitacoraRegistroInsertar", new
                {
                    entity.IdRegistro,
                    entity.IdUsuario,
                    entity.FechaHoraIngreso,
                    entity.FechaHoraSalida,
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
