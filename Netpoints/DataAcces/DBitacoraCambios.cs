using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAcces
{
    public class DBitacoraCambios
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        #region Agregar
        public int Agregar(EBitacoraCambios obj)//Viene de la vista obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    BitacoraCambios Objbd = new BitacoraCambios();//Viene de la base de datos
                    Objbd.IdUsuario = obj.IdUsuario;
                    Objbd.FechaHora = obj.FechaHora;
                    Objbd.Tipo = obj.Tipo;
                    Objbd.Detalle = obj.Detalle;
                    db.BitacoraCambios.Add(Objbd);

                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        return Resultado;
                    }
                    else
                    {
                        Ts.Dispose();
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Listar
        public List<EBitacoraCambios> Mostrar()
        {
            try
            {
                List<EBitacoraCambios> Obj = new List<EBitacoraCambios>();
                var Objbd = db.BitacoraCambios.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new EBitacoraCambios()
                    {
                        IdCambio = Item.IdCambio,
                        IdUsuario = Item.IdUsuario,
                        FechaHora = Item.FechaHora,
                        Tipo = Item.Tipo,
                        Detalle = Item.Detalle
                    });
                }
                return Obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
