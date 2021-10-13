using Entidades;
using System;
using System.Collections.Generic;
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
        public EUsuario Mostrar_Detallado(int id)
        {
            try
            {
                using (db)
                {
                    EUsuario Obj = new EUsuario();
                    var Objbd = db.Usuarios.Where(a => a.IdUsuario == id).FirstOrDefault();
                    Obj.IdRol = Objbd.IdRol;
                    Obj.Contrasena = Objbd.Contrasena;
                    Obj.Email = Objbd.Email;
                    Obj.Estado = Objbd.Estado;
                    Obj.Identificacion = Objbd.Identificacion;
                    Obj.IdUsuario = Objbd.IdUsuario;
                    Obj.Nombre = Objbd.Nombre;
                    Obj.Telefono = Objbd.Telefono;
                    Obj.Usuario = Objbd.Usuario;
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
        public int Modificar(EUsuario Obj)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Usuarios Objbd = db.Usuarios.Find(Obj.IdUsuario);
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    Objbd.IdRol = Obj.IdRol;
                    if (Obj.Contrasena != "Contra001")
                    {
                        Objbd.Contrasena = Obj.Contrasena;
                    }
                    Objbd.Email = Obj.Email;
                    Objbd.Estado = Obj.Estado;
                    Objbd.Nombre = Obj.Nombre;
                    Objbd.Telefono = Obj.Telefono;
                    Objbd.Usuario = Obj.Usuario;
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
        public int Desactivar(int IdUsuario)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    Usuarios Objbd = db.Usuarios.Find(IdUsuario);
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    Objbd.Estado = false;
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
