using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NBitacoraCambios
    {
        public List<EBitacoraCambios> Mostrar()
        {
            try
            {
                DBitacoraCambios db = new DBitacoraCambios();
                return db.Mostrar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
