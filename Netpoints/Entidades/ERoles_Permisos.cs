using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ERoles_Permisos
    {
        public int IdPermiso { get; set; }
        public Nullable<int> Id_Rol { get; set; }
        public Nullable<int> Modulo { get; set; }
        public string Agregar { get; set; }
        public string Modificar { get; set; }
        public string Eliminar { get; set; }

    }
}
