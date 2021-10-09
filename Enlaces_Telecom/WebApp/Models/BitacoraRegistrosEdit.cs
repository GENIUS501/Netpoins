using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BitacoraRegistrosEdit
    {
        public BitacoraRegistrosEntity bitacoraRegistros { get; set; }
        public List<UsuariosEntity> ddlUsuarios { get; set; }
    }
}