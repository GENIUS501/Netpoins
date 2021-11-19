using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    db.Entry(Objbd).State = EntityState.Modified;
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
        public List<EBitacoraRegistros> Mostrar()
        {
            try
            {
                List<EBitacoraRegistros> Obj = new List<EBitacoraRegistros>();
                var Objbd = db.BitacoraRegistros.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new EBitacoraRegistros()
                    {
                        IdRegistro = Item.IdRegistro,
                        IdUsuario = Item.IdUsuario,
                        FechaHoraIngreso = Item.FechaHoraIngreso,
                        FechaHoraSalida = Item.FechaHoraSalida
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
