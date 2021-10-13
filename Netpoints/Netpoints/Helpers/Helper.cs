using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Netpoints.Helpers
{
    public class Helper
    {
        public static string EncodePassword(string originalPassword)
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();

                byte[] inputBytes = (new UnicodeEncoding()).GetBytes(originalPassword);
                byte[] hash = sha1.ComputeHash(inputBytes);

                return Convert.ToBase64String(hash);
            }

            //public static void registrar_transaccion(string modulo, string accion)
            //{
            //    using (var db = new SCIVEntities())
            //    {
            //        Tab_Usuarios Ent_Usuario;
            //        Tab_Bitacora_Movimientos Mov = new Tab_Bitacora_Movimientos();
            //        Mov.Fecha_Hora = DateTime.Now;
            //        Mov.Modulo_Afectado = modulo;
            //        Ent_Usuario = new Tab_Usuarios();
            //        Ent_Usuario = System.Web.HttpContext.Current.Session["User"] as Tab_Usuarios;
            //        //Ent_Usuario = (Tab_Usuarios)HttpContext.Current.Session["User"];
            //        Mov.NickName = Ent_Usuario.NickName;
            //        Mov.Tipo_Movimiento = accion;
            //        db.Tab_Bitacora_Movimientos.Add(Mov);
            //        db.SaveChanges();
            //    }
            //}

            //public static int ingresar(string usuario)
            //{
            //    try
            //    {
            //        using (var db = new SCIVEntities())
            //        {
            //            Tab_Bitacora_Sesiones Ses = new Tab_Bitacora_Sesiones();
            //            Ses.Ingreso = DateTime.Now;
            //            Ses.NickName = usuario;
            //            db.Tab_Bitacora_Sesiones.Add(Ses);
            //            db.SaveChanges();
            //            return Ses.Id_Sesion;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return 0;
            //    }

            //}

            //public static int Salir(int id_session)
            //{
            //    try
            //    {
            //        //  Session["id_sesion"];
            //        using (var db = new SCIVEntities())
            //        {

            //            //Esto llena la entidad con los datos correspondientes a la entidad traida de la bd
            //            Tab_Bitacora_Sesiones registro = new Tab_Bitacora_Sesiones();
            //            registro = db.Tab_Bitacora_Sesiones.Where(b => b.Id_Sesion == id_session).FirstOrDefault();
            //            //Asigna los valores traidos por la entidad traida de la vista a la entidad traida de la base de datos
            //            //registro.Id_Perfil = a.Nombre;
            //            registro.Salida = DateTime.Now;
            //            db.SaveChanges();
            //            return 1;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return 0;
            //    }

            //}

    }
}