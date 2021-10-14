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
    public class DProveedores
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        #region Agregar
        public int Agregar(EProveedores obj)//Viene de la vista obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Proveedores Objbd = new Proveedores();//Viene de la base de datos
                    Objbd.Comentario = obj.Comentario;
                    Objbd.Contacto = obj.Contacto;
                    Objbd.Email = obj.Email;
                    Objbd.NombreEmpresa = obj.NombreEmpresa;
                    Objbd.Telefono = obj.Telefono;
                    db.Proveedores.Add(Objbd);

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
        public EProveedores Mostrar_Detallado(int id)
        {
            try
            {
                using (db)
                {
                    EProveedores Obj = new EProveedores();
                    var Objbd = db.Proveedores.Where(a => a.IdProveedor == id).FirstOrDefault();
                    Obj.Comentario = Objbd.Comentario;
                    Obj.Contacto = Objbd.Contacto;
                    Obj.Email = Objbd.Email;
                    Obj.IdProveedor = Objbd.IdProveedor;
                    Obj.NombreEmpresa = Objbd.NombreEmpresa;
                    Obj.Telefono = Objbd.Telefono;
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
        public int Modificar(EProveedores Obj)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    var Objbd = db.Proveedores.Find(Obj.IdProveedor);
                    Objbd.Comentario = Obj.Comentario;
                    Objbd.Contacto = Obj.Contacto;
                    Objbd.Email = Obj.Email;
                    Objbd.NombreEmpresa = Obj.NombreEmpresa;
                    Objbd.Telefono = Obj.Telefono;
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
        public int Eliminar(int Id)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Proveedores Objbd = db.Proveedores.Find(Id);
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
        public List<EProveedores> Mostrar()
        {
            try
            {
                List<EProveedores> Obj = new List<EProveedores>();
                var Objbd = db.Proveedores.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new EProveedores()
                    {
                        Comentario = Item.Comentario,
                        Contacto = Item.Contacto,
                        Email = Item.Email,
                        IdProveedor = Item.IdProveedor,
                        NombreEmpresa = Item.NombreEmpresa,
                        Telefono = Item.Telefono
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
