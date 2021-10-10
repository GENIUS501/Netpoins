using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAcces
{
    public class DRol
    {
        Enlaces_TelecomEntities db = new Enlaces_TelecomEntities();

        #region "Agregar"
        public int Agregar(ERol obj)//Viene de la vista obj
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

    }
}
