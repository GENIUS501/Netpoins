using DataAccess;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBL
{
    public interface IReporteUbicacionService : IDisposable
    {
        List<ReporteUbicacionEntity> Obtenerddl();
    }
    public class ReporteUbicacionService : IReporteUbicacionService
    {
        public IBD sql = new BD("Conn");
        public void Dispose()
        {
            sql = null;
        }
        public List<ReporteUbicacionEntity> Obtenerddl()
        {
            try
            {
                var result = sql.Query<ReporteUbicacionEntity>("ReporteUbicacionLista");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}