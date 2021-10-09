using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBL;

namespace WebApp
{
    public class IApp
    {
        public static IUsuariosService usuariosService => new UsuariosService();
        public static IRolesService rolesService => new RolesService();
        public static IProveedoresService proveedoresService => new ProveedoresService();
        public static IRedesService redesService => new RedesService();
        public static IOficinasService oficinasService => new OficinasService();
        public static IEnlacesService enlacesService => new EnlacesService();
        public static IBitacoraRegistrosService bitacoraRegistrosService => new BitacoraRegistrosService();
        public static IBitacoraCambiosService bitacoraCambiossService => new BitacoraCambiosService();
        public static IReporteUbicacionService reporteUbicacionService => new ReporteUbicacionService();
    }
}