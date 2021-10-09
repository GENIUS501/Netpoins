using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ReporteUbicacionEdit
    {

            public ReporteUbicacionEntity reporteUbicacion { get; set; }
            public List<UsuariosEntity> ddlUsuarios { get; set; }
            public List<OficinasEntity> ddlOficinas { get; set; }
            public List<EnlacesEntity> ddlEnlaces { get; set; }
    }
}