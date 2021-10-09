﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class UsuariosEntity : EN 
    {
        public int? IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public RolesEntity Roles { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public bool Estado { get; set; }

        public UsuariosEntity()
        {
            Roles = Roles ?? new RolesEntity();
        }
    }
}
