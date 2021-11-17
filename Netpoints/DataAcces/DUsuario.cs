using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAcces
{
    public class DUsuario
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        DBitacoraCambios Bitacora = new DBitacoraCambios();
        EBitacoraCambios EntidadBitacora = new EBitacoraCambios();

        #region Agregar
        public int Agregar(EUsuario obj, int Usuario)//Viene de la vista obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Usuarios Objbd = new Usuarios();//Viene de la base de datos
                    Objbd.Identificacion = obj.Identificacion;
                    Objbd.IdRol = obj.IdRol;
                    Objbd.Estado = obj.Estado;
                    Objbd.Nombre = obj.Nombre;
                    Objbd.Telefono = obj.Telefono;
                    Objbd.Usuario = obj.Usuario;
                    Objbd.Contrasena = obj.Contrasena;
                    Objbd.Email = obj.Email;
                    db.Usuarios.Add(Objbd);

                    int Resultado = db.SaveChanges();//Commit

                    if (Resultado > 0)
                    {
                        Ts.Complete();
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Agregar";
                        EntidadBitacora.Detalle = "Usuarios";
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
        public int Modificar(EUsuario Obj, int Usuario)
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
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Editar";
                        EntidadBitacora.Detalle = "Usuarios";
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
        public int Desactivar(int IdUsuario, int Usuario)
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
                        EntidadBitacora.IdUsuario = Usuario;
                        EntidadBitacora.Tipo = "Desactivar";
                        EntidadBitacora.Detalle = "Usuarios";
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
        public List<EUsuario> Mostrar()
        {
            try
            {
                List<EUsuario> Obj = new List<EUsuario>();
                var Objbd = db.Usuarios.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new EUsuario()
                    {
                        IdRol = Item.IdRol,
                        Nombre = Item.Nombre,
                        Contrasena = Item.Contrasena,
                        Email = Item.Email,
                        Estado = Item.Estado,
                        Identificacion = Item.Identificacion,
                        IdUsuario = Item.IdUsuario,
                        Telefono = Item.Telefono,
                        Usuario = Item.Usuario
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

        #region Metodos Personalizados
        public string Nombre_Rol(int id)
        {
            try
            {
                using (db)
                {
                    //Retorna el nombre del perfil correspondiente al id enviado al metodo
                    return db.Roles.Find(id).Rol;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        public async Task<bool> Verificar(string id)
        {
            try
            {
                using (db)
                {
                    //Retorna el nombre del perfil correspondiente al id enviado al metodo
                    var Identificacion = await db.Usuarios.Where(x => x.Identificacion == id).FirstOrDefaultAsync();
                    if (Identificacion != null)
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
        public List<ERol> llenarRoles()
        {
            try
            {
                List<ERol> lista1 = new List<ERol>();
                //llena la lista que a su vez es una entidad
                using (db)
                {
                    var Rol = db.Roles.Where(x => x.Estado == true).ToList();
                    foreach (var item in Rol)
                    {
                        lista1.Add(new ERol { IdRol = item.IdRol, Descripcion = item.Descripcion, Estado = item.Estado, Rol = item.Rol });
                    }
                }
                return lista1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EUsuario Login(string Usuario, string Pass)
        {
            try
            {
                Usuarios Objbd = new Usuarios();
                using (db)
                {
                    Objbd = db.Usuarios.Where(x => x.Usuario == Usuario && x.Contrasena == Pass && x.Estado == true).FirstOrDefault();
                }
                if (Objbd != null)
                {
                    EUsuario Obj = new EUsuario();
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
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}
