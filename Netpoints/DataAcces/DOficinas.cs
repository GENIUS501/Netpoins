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
    public class DOficinas
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        EBitacoraCambios EntidadBitacora = new EBitacoraCambios();
        DBitacoraCambios Bitacora = new DBitacoraCambios();

        #region Agregar
        public int Agregar(EOficinas Obj,int Usuario)//Viene de la vista Obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Oficinas Objbd = new Oficinas();//Viene de la base de datos
                    Objbd.Comentario = Obj.Comentario;
                    Objbd.NombreOficina = Obj.NombreOficina;
                    Objbd.Provincia = Obj.Provincia;
                    Objbd.UE = Obj.UE;
                    db.Oficinas.Add(Objbd);

                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Agregar";
                        EntidadBitacora.Detalle = "Sitios";
                        EntidadBitacora.FechaHora = DateTime.Now;
                        Bitacora.Agregar(EntidadBitacora);
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
        public EOficinas Mostrar_Detallado(int id)
        {
            try
            {
                using (db)
                {
                    EOficinas Obj = new EOficinas();
                    var Objbd = db.Oficinas.Where(a => a.IdOficina == id).FirstOrDefault();
                    Obj.Comentario = Objbd.Comentario;
                    Obj.NombreOficina = Objbd.NombreOficina;
                    Obj.Provincia = Objbd.Provincia;
                    Obj.UE = Objbd.UE;
                    Obj.IdOficina = Objbd.IdOficina;
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
        public int Modificar(EOficinas Obj,int Usuario)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    var Objbd = db.Oficinas.Find(Obj.IdOficina);
                    Objbd.Comentario = Obj.Comentario;
                    Objbd.NombreOficina = Obj.NombreOficina;
                    Objbd.Provincia = Obj.Provincia;
                    Objbd.UE = Obj.UE;
                    //Guarda los cambios en bd
                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Editar";
                        EntidadBitacora.Detalle = "Sitios";
                        EntidadBitacora.FechaHora = DateTime.Now;
                        Bitacora.Agregar(EntidadBitacora);
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
        public int Eliminar(int Id,int Usuario)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Oficinas Objbd = db.Oficinas.Find(Id);
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    db.Entry(Objbd).State = EntityState.Deleted;
                    //Guarda los cambios en bd
                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Agregar";
                        EntidadBitacora.Detalle = "Sitios";
                        EntidadBitacora.FechaHora = DateTime.Now;
                        Bitacora.Agregar(EntidadBitacora);
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
        public List<EOficinas> Mostrar()
        {
            try
            {
                List<EOficinas> Obj = new List<EOficinas>();
                var Objbd = db.Oficinas.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new EOficinas()
                    {
                        Comentario = Item.Comentario,
                        NombreOficina = Item.NombreOficina,
                        Provincia = Item.Provincia,
                        UE = Item.UE,
                        IdOficina = Item.IdOficina
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
