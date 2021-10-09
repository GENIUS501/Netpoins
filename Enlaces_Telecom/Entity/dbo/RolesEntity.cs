using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
   public class RolesEntity :EN
    {
        public int? IdRol { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }
}
