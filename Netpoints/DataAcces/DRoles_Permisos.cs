using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAcces
{
    public class DRoles_Permisos
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        #region Agregar
        public int Agregar(List<ERoles_Permisos> obj)//Viene de la vista obj
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Roles_Permisos Objbd = new Roles_Permisos();//Viene de la base de datos
                    //Objbd.Comentario = obj.Comentario;
                    //Objbd.Contacto = obj.Contacto;
                    //Objbd.Email = obj.Email;
                    //Objbd.NombreEmpresa = obj.NombreEmpresa;
                    //Objbd.Telefono = obj.Telefono;
                    string Objason = JsonConvert.SerializeObject(obj);
                    List<Roles_Permisos> Permisos = JsonConvert.DeserializeObject<List<Roles_Permisos>>(Objason);
                    db.Roles_Permisos.AddRange(Permisos);

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
        public ERoles_Permisos Mostrar_Detallado(int id)
        {
            try
            {
                using (db)
                {
                    ERoles_Permisos Obj = new ERoles_Permisos();
                    var Objbd = db.Roles_Permisos.Where(a => a.IdPermiso == id).FirstOrDefault();
                    Obj.IdPermiso = Objbd.IdPermiso;
                    Obj.Id_Rol = Objbd.Id_Rol;
                    Obj.Modificar = Objbd.Modificar;
                    Obj.Modulo = Objbd.Modulo;
                    Obj.Agregar = Objbd.Agregar;
                    Obj.Eliminar = Objbd.Eliminar;
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
        public int Modificar(ERoles_Permisos Obj)
        {
            try
            {
                using (TransactionScope Ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
                    var Objbd = db.Roles_Permisos.Find(Obj.IdPermiso);
                    Objbd.Id_Rol = Obj.Id_Rol;
                    Objbd.Modificar = Obj.Modificar;
                    Objbd.Modulo = Obj.Modulo;
                    Objbd.Agregar = Obj.Agregar;
                    Objbd.Eliminar = Obj.Eliminar;
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
                    var Objbd = db.Roles_Permisos.Where(x => x.Id_Rol == Id).ToList();
                    //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
                    db.Roles_Permisos.RemoveRange(Objbd);
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
        public List<ERoles_Permisos> Mostrar()
        {
            try
            {
                List<ERoles_Permisos> Obj = new List<ERoles_Permisos>();
                var Objbd = db.Roles_Permisos.ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new ERoles_Permisos()
                    {
                        IdPermiso = Item.IdPermiso,
                        Id_Rol = Item.Id_Rol,
                        Modificar = Item.Modificar,
                        Modulo = Item.Modulo,
                        Agregar = Item.Agregar,
                        Eliminar = Item.Eliminar,
                    });
                }
                return Obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ERoles_Permisos> Mostrar(int Id)
        {
            try
            {
                List<ERoles_Permisos> Obj = new List<ERoles_Permisos>();
                var Objbd = db.Roles_Permisos.Where(x => x.Id_Rol == Id).ToList();
                foreach (var Item in Objbd)
                {
                    Obj.Add(new ERoles_Permisos()
                    {
                        IdPermiso = Item.IdPermiso,
                        Id_Rol = Item.Id_Rol,
                        Modificar = Item.Modificar,
                        Modulo = Item.Modulo,
                        Agregar = Item.Agregar,
                        Eliminar = Item.Eliminar,
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
