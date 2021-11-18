using DataAcces;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
    public class NBitacoraRegistro
    {
        public int Agregar(EBitacoraRegistros Obj)
        {
            try
            {
                DBitacoraRegistros db = new DBitacoraRegistros();
                return db.Agregar(Obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Modificar(int Idsesion)
        {
            try
            {
                DBitacoraRegistros db = new DBitacoraRegistros();
                return db.Modificar(Idsesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EBitacoraRegistros> Mostrar()
        {
            try
            {
                DBitacoraRegistros db = new DBitacoraRegistros();
                return db.Mostrar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
