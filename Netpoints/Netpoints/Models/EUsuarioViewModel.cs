using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netpoints.Models
{
    public class EUsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Estado { get; set; }
    }
}