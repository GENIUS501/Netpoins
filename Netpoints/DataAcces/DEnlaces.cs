﻿using Entidades;
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
        #region Agregar
        public int Agregar(EEnlaces Obj)//Viene de la vista Obj
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
                    var Objbd = db.Enlaces.Where(a => a.IdRed == id).FirstOrDefault();
                    Obj.Comentario = Objbd.Comentario;
                    Obj.IdOficina = Objbd.IdOficina;
                    Obj.IdProveedor = Objbd.IdProveedor;
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
        public int Modificar(EEnlaces Obj)
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
                    Enlaces Objbd = db.Enlaces.Find(Id);
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
        #endregion
    }
}