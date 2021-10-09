using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class UsuariosEdit
    {
        public UsuariosEntity usuarios { get; set; }
        public List<RolesEntity> ddlRoles { get; set; }
    }
}