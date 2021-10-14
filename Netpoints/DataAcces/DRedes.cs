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
    public class DRedes
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        #region Agregar
        public int Agregar(ERedes Obj)//Viene de la vista Obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Redes Objbd = new Redes();//Viene de la base de datos
                    Objbd.Comentario = Obj.Comentario;
                    Objbd.Bandwidth = Obj.Bandwidth;
                    Objbd.Gateway = Obj.Gateway;
                    Objbd.Interface = Obj.Interface;
                    Objbd.Linea = Obj.Linea;
                    Objbd.MedioEnlace = Obj.MedioEnlace;
                    Objbd.TipoEnlace = Obj.TipoEnlace;
                    db.Redes.Add(Objbd);

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

        #region Mostrar Detallado
        public ERedes Mostrar_Detallado(int id)
        {
            try
            {
                using (db)
                {
                    ERedes Obj = new ERedes();
                    var Objbd = db.Redes.Where(a => a.IdRed == id).FirstOrDefault();
                    Obj.Comentario = Objbd.Comentario;
                    Obj.Bandwidth = Objbd.Bandwidth;
                    Obj.Gateway = Objbd.Gateway;
                    Obj.Interface = Objbd.Interface;
                    Obj.Linea = Objbd.Linea;
                    Obj.MedioEnlace = Objbd.MedioEnlace;
                    Obj.TipoEnlace = Objbd.TipoEnlace;
                    Obj.IdRed = Objbd.IdRed;
                    return Obj;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region Modificar
        public int Modificar(ERedes Obj)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    var Objbd = db.Redes.Find(Obj.IdRed);
                    Objbd.Comentario = Obj.Comentario;
                    Objbd.Bandwidth = Obj.Bandwidth;
                    Objbd.Gateway = Obj.Gateway;
                    Objbd.Interface = Obj.Interface;
                    Objbd.Linea = Obj.Linea;
                    Objbd.MedioEnlace = Obj.MedioEnlace;
                    Objbd.TipoEnlace = Obj.TipoEnlace;
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

        #region Eliminar
        public int Eliminar(int Id)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Redes Objbd = db.Redes.Find(Id);
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    db.Entry(Objbd).State = EntityState.Deleted;
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
        public List<ERedes> Mostrar()
        {
            try
            {
                List<ERedes> Obj = new List<ERedes>();
                var Objbd = db.Redes.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new ERedes()
                    {
                        Comentario = Item.Comentario,
                        Bandwidth = Item.Bandwidth,
                        Gateway = Item.Gateway,
                        Interface = Item.Interface,
                        Linea = Item.Linea,
                        MedioEnlace = Item.MedioEnlace,
                        TipoEnlace = Item.TipoEnlace,
                        IdRed = Item.IdRed
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
