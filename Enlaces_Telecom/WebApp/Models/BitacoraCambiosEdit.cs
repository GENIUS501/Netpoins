using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BitacoraCambiosEdit
    {
        public BitacoraCambiosEntity bitacoraCambios { get; set; }
        public List<UsuariosEntity> ddlUsuarios { get; set; }
    }
}