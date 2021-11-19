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
    public class DRol
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        EBitacoraCambios EntidadBitacora = new EBitacoraCambios();
        DBitacoraCambios Bitacora = new DBitacoraCambios();
        #region "Agregar"
        public int Agregar(ERol obj,int Usuario)//Viene de la vista obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Roles Objbd = new Roles();//Viene de la base de datos
                    Objbd.Rol = obj.Rol;
                    Objbd.Descripcion = obj.Descripcion;
                    Objbd.Estado = obj.Estado;
                    db.Roles.Add(Objbd);
                    db.SaveChanges();//Commit
                    int Resultado = Objbd.IdRol;

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Agregar";
                        EntidadBitacora.Detalle = "Roles";
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
        public ERol Mostrar_Detallado(int id)
        {
            try
            {
                using (db)
                {
                    ERol Obj = new ERol();
                    var Objbd = db.Roles.Where(a => a.IdRol == id).FirstOrDefault();
                    Obj.IdRol = Objbd.IdRol;
                    Obj.Rol = Objbd.Rol;
                    Obj.Descripcion = Objbd.Descripcion;
                    Obj.Estado = Objbd.Estado;
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
        public int Modificar(ERol Obj,int Usuario)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Roles Objbd = db.Roles.Find(Obj.IdRol);
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    Objbd.Rol = Obj.Rol;
                    Objbd.Descripcion = Obj.Descripcion;
                    Objbd.Estado = Obj.Estado;
                    //Guarda los cambios en bd
                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Editar";
                        EntidadBitacora.Detalle = "Roles";
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
        public int Desactivar(int IdRol,int Usuario)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Roles Objbd = db.Roles.Find(IdRol);
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    Objbd.Estado = false;
                    //Guarda los cambios en bd
                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Desactivar";
                        EntidadBitacora.Detalle = "Roles";
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
        public List<ERol> Mostrar()
        {
            try
            {
                List<ERol> Obj = new List<ERol>();
                var Objbd = db.Roles.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new ERol()
                    {
                        IdRol = Item.IdRol,
                        Rol = Item.Rol,
                        Descripcion = Item.Descripcion,
                        Estado = Item.Estado
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

        #region Metodos personalizados
        public async Task<bool> Verificar(string id)
        {
            try
            {
                using (db)
                {
                    //Retorna el nombre del perfil correspondiente al id enviado al metodo
                    var Objbd = await db.Roles.Where(x => x.Rol == id).FirstOrDefaultAsync();
                    if (Objbd != null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
