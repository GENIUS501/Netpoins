﻿using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAcces
{
    public class DUsuario
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();
        #region Agregar
        public int Agregar(EUsuario obj)//Viene de la vista obj
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
        public int Modificar(ERol Obj)
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
        public int Desactivar(int IdRol)
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
        public List<ERol> llenarRoles()
        {
            try
            {
                List<ERol> lista1;
                //llena la lista que a su vez es una entidad
                using (db)
                {
                    //consulta
                    lista1 =
                        (from d in db.Roles
                         select new ERol
                         {
                             IdRol = d.IdRol,
                             Descripcion = d.Descripcion
                         }).ToList();
                }
                return lista1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
    }
}
