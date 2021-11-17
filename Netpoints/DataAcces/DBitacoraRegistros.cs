using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAcces
{
    public class DBitacoraRegistros
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        #region Agregar
        public int Agregar(EBitacoraRegistros obj)//Viene de la vista obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    BitacoraRegistros Objbd = new BitacoraRegistros();//Viene de la base de datos
                    Objbd.IdUsuario = obj.IdUsuario;
                    Objbd.FechaHoraIngreso = obj.FechaHoraIngreso;
                    db.BitacoraRegistros.Add(Objbd);
                    db.SaveChanges();//Commit
                    int Resultado = Objbd.IdRegistro;

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

        #region Modificar
        public int Modificar(int IdRegistro)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    var Objbd = db.BitacoraRegistros.Find(IdRegistro);
                    Objbd.FechaHoraSalida = DateTime.Now;
                    //Guarda los cambios en bd
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
