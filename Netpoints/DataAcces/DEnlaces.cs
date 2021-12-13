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
    public class DEnlaces
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        EBitacoraCambios EntidadBitacora = new EBitacoraCambios();
        DBitacoraCambios Bitacora = new DBitacoraCambios();

        #region Agregar
        public int Agregar(EEnlaces Obj, int Usuario)//Viene de la vista Obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Enlaces Objbd = new Enlaces();//Viene de la base de datos
                    Objbd.Comentario = Obj.Comentario;
                    Objbd.IdOficina = Obj.IdOficina;
                    Objbd.IdProveedor = Obj.IdProveedor;
                    Objbd.IdRed = Obj.IdRed;
                    db.Enlaces.Add(Objbd);

                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Agregar";
                        EntidadBitacora.Detalle = "Enlaces";
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
        public EEnlaces Mostrar_Detallado(int id)
        {
            try
            {
                using (db)
                {
                    EEnlaces Obj = new EEnlaces();
                    var Objbd = db.Enlaces.Where(a => a.IdEnlace == id).FirstOrDefault();
                    Obj.Comentario = Objbd.Comentario;
                    Obj.IdOficina = Objbd.IdOficina;
                    Obj.IdProveedor = Objbd.IdProveedor;
                    Obj.IdRed = Objbd.IdRed;
                    Obj.IdEnlace = Objbd.IdEnlace;
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
        public int Modificar(EEnlaces Obj, int Usuario)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    var Objbd = db.Enlaces.Find(Obj.IdEnlace);
                    Objbd.Comentario = Obj.Comentario;
                    Objbd.IdOficina = Obj.IdOficina;
                    Objbd.IdProveedor = Obj.IdProveedor;
                    Objbd.IdRed = Obj.IdRed;
                    db.Entry(Objbd).State = EntityState.Modified;
                    //Guarda los cambios en bd
                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Editar";
                        EntidadBitacora.Detalle = "Enlaces";
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
        public int Eliminar(int Id, int Usuario)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Enlaces Objbd = db.Enlaces.Find(Id);
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    db.Entry(Objbd).State = EntityState.Deleted;
                    //Guarda los cambios en bd
                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Eliminar";
                        EntidadBitacora.Detalle = "Enlaces";
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
        public List<EEnlaces> Mostrar()
        {
            try
            {
                List<EEnlaces> Obj = new List<EEnlaces>();
                var Objbd = db.Enlaces.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new EEnlaces()
                    {
                        Comentario = Item.Comentario,
                        IdOficina = Item.IdOficina,
                        IdProveedor = Item.IdProveedor,
                        IdRed = Item.IdRed,
                        IdEnlace = Item.IdEnlace
                    });
                }
                return Obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EEnlaces> MostrarProveedores(int Proveedor)
        {
            try
            {
                using (db)
                {
                    List<EEnlaces> Obj = new List<EEnlaces>();
                    var Objbd = db.Enlaces.Where(x => x.IdProveedor == Proveedor).ToList();
                    foreach (var Item in Objbd)
                    {
                            Obj.Add(new EEnlaces()
                            {
                                Comentario = Item.Comentario,
                                IdOficina = Item.IdOficina,
                                IdProveedor = Item.IdProveedor,
                                IdRed = Item.IdRed,
                                IdEnlace = Item.IdEnlace
                            });
                    }
                    return Obj;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EEnlaces> Mostrar(string Provincia)
        {
            try
            {
                List<EEnlaces> Obj = new List<EEnlaces>();
                var Objbd = db.Enlaces.Where(x => x.Oficinas.Provincia == Provincia).ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new EEnlaces()
                    {
                        Comentario = Item.Comentario,
                        IdOficina = Item.IdOficina,
                        IdProveedor = Item.IdProveedor,
                        IdRed = Item.IdRed,
                        IdEnlace = Item.IdEnlace
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
